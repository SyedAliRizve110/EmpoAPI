using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.EmployeeService.Application.Enums;
using Empo.EmployeeService.Application.Media;
using Empo.EmployeeService.Application.Media.CreatePost;
using Empo.EmployeeService.Infrastructure.Data.Entities.Media;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data.Services;

public class MediaService : IMediaService
{
    private readonly EmployeeContext _dbContext;
    private readonly IFileStorageService _storage;

    private const string MediaContainer = "post-media";
    private const long MaxFileSizeBytes = 50 * 1024 * 1024;
    private static readonly string[] AllowedImageTypes = { "image/jpeg", "image/png", "image/gif" };
    private static readonly string[] AllowedVideoTypes = { "video/mp4", "video/quicktime", "video/webm" };

    public MediaService(EmployeeContext dbContext, IFileStorageService storage)
    {
        _dbContext = dbContext;
        _storage = storage;
    }
    public async Task<CommentModel> AddCommentAsync(CreateCommentRequest request, Guid postId, Guid currentUserId)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
            throw new BadRequestException("Comment text cannot be empty.");

        var postExists = await _dbContext.Post.AnyAsync(p => p.Id == postId && !p.IsDeleted);

        if (!postExists)
            throw new NotFoundException("Post", postId);

        var comment = new PostCommentEntity
        {
            PostId = postId,
            UserId = currentUserId,
            Text = request.Text
        };

        _dbContext.PostComment.Add(comment);
        await _dbContext.SaveChangesAsync();

        var user = await _dbContext.User.FindAsync(currentUserId);

        return new CommentModel
        {
            Id = comment.Id,
            UserId = comment.UserId,
            CommentedByName = user?.UserName ?? "",
            Text = comment.Text,
            CreatedAt = comment.DateCreated
        };
    }

    public async Task<PostModel> CreatePostAsync(CreatePostRequest request, Guid currentUserId)
    {
        var hasText = !string.IsNullOrWhiteSpace(request.Text);
        var hasMedia = request.MediaFiles is { Count: > 0 };

        if (!hasText && !hasMedia)
        {
            throw new MediaNotFoundException("Post must contain text or media.");
        }

        var post = new PostEntity
        {
            Id = Guid.NewGuid(),
            UserId = currentUserId,
            Text = request.Text
        };

        if (hasMedia)
        {
            int order = 0;
            foreach (var file in request.MediaFiles)
            {
                ValidateMediaFile(file);
                var mediaType = AllowedImageTypes.Contains(file.ContentType) ? MediaType.Image 
                    : AllowedVideoTypes.Contains(file.ContentType) ? MediaType.Video
                    : throw new BadRequestException($"File type '{file.ContentType}' is not allowed.");

                await using var stream = file.OpenReadStream();
                var stored = await _storage.SaveAsync(stream, file.FileName, file.ContentType, MediaContainer);

                post.Media.Add(new PostMediaEntity
                {
                    Id = Guid.NewGuid(),
                    PostId = post.Id,
                    MediaType = mediaType,
                    StorageKey = stored.StorageKey,
                    Url = stored.PublicUrl,
                    ContentType = stored.ContentType,
                    SizeInBytes = stored.SizeInBytes,
                    SortOrder = order++,
                });
            }
        }

        _dbContext.Post.Add(post);
        await _dbContext.SaveChangesAsync();

        var user = await _dbContext.User.FindAsync(currentUserId);

        return new PostModel
        {
            Id = post.Id,
            UserId = post.User.Id,
            PostedByName = user?.UserName ?? "",
            Text = post.Text,
            Media = post.Media.Select(m => new PostMediaModel
            {
                Id = m.Id,
                MediaType = m.MediaType.ToString(),
                Url = m.Url,
                SortOrder = m.SortOrder
            }).ToList(),
            LikeCount = 0,
            CommentCount = 0,
            IsLikedByCurrentUser = false,
            CreatedAt = post.DateCreated
        };

    }

    private static void ValidateMediaFile(IFormFile file)
    {
        if (file.Length == 0)
            throw new BadHttpRequestException("upload file is empty");

        if (file.Length > MaxFileSizeBytes)
            throw new BadRequestException($"File '{file.FileName}' exceeds the 50 MB limit.");
        var isAllowed = AllowedImageTypes.Contains(file.ContentType) || AllowedVideoTypes.Contains(file.ContentType);
        if (!isAllowed)
            throw new BadRequestException($"File type '{file.ContentType}' is not allowed.");
    }

    public async Task DeletePostAsync(Guid postId, Guid currentUserId, bool isModerator)
    {
        var post = await _dbContext.Post.FirstOrDefaultAsync(p => p.Id == postId && !p.IsDeleted);
        if (post is null)
            throw new NotFoundException("Post", postId);
        if (post.UserId != currentUserId && !isModerator)
            throw new ForbiddenException("You can only delete your own posts");
        post.IsDeleted = true;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<CommentModel>> GetCommentsAsync(Guid postId)
    {
        return await _dbContext.PostComment
            .Where(c => c.PostId == postId && !c.IsDeleted)
            .OrderBy(c => c.DateCreated)
            .Select(c => new CommentModel
            {
                Id = c.Id,
                UserId = c.UserId,
                CommentedByName = c.User.UserName,
                Text = c.Text,
                CreatedAt = c.DateCreated
            }).ToListAsync();
    }

    public async Task<GetFeedResponse> GetFeedAsync(GetFeedRequest request, Guid currentUserId)
    {
        var query = _dbContext.Post.Where(p => !p.IsDeleted).OrderByDescending(p => p.DateCreated);

        var totalCount = await query.CountAsync();

        var posts = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(p => new PostModel
            {
                Id = p.Id,
                UserId = p.UserId,
                PostedByName = p.User.UserName,
                Text = p.Text,
                Media = p.Media
                .OrderBy(m => m.SortOrder)
                .Select(m => new PostMediaModel
                {
                    Id = m.Id,
                    MediaType = m.MediaType.ToString(),
                    Url = m.Url,
                    SortOrder = m.SortOrder
                }).ToList(),
                LikeCount = p.Likes.Count,
                CommentCount = p.Comments.Count(c => !c.IsDeleted),
                IsLikedByCurrentUser = p.Likes.Any(l => l.UserId == currentUserId),
                CreatedAt = p.DateCreated
            }).ToListAsync();
        return new GetFeedResponse
        {
            Posts = posts,
            TotalCount = totalCount,
            HasMore = request.PageNumber * request.PageSize < totalCount,
        };
    }

    public async Task<bool> ToggleLikeAsync(Guid postId, Guid currentUserId)
    {
        var postExists = await _dbContext.Post.AnyAsync(p => p.Id == postId && !p.IsDeleted);
        if (!postExists)
            throw new NotFoundException("Post", postId);

        var existingLike = await _dbContext.PostLike.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == currentUserId);

        if (existingLike != null)
        {
            _dbContext.PostLike.Remove(existingLike);
            await _dbContext.SaveChangesAsync();
            return false;
        }

        _dbContext.PostLike.Add(new PostLikeEntity
        {
            PostId = postId,
            UserId = currentUserId
        });
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
