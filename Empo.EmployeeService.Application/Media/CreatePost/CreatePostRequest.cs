using Microsoft.AspNetCore.Http;

namespace Empo.EmployeeService.Application.Media.CreatePost;

public class CreatePostRequest
{
    public string? Text { get; set; }
    public List<IFormFile>? MediaFiles { get; set; }
}
public class CreateCommentRequest
{
    public string Text { get; set; } = string.Empty;
}
public class GetFeedRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
public class GetFeedResponse
{
    public List<PostModel> Posts { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasMore { get; set; }
}
public class PostMediaModel
{
    public Guid Id { get; set; }
    public string MediaType { get; set; } = string.Empty; // "Image" / "Video"
    public string Url { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
public class PostModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string PostedByName { get; set; } = string.Empty;
    public string? Text { get; set; }
    public List<PostMediaModel> Media { get; set; } = new();
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    public bool IsLikedByCurrentUser { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CommentModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string CommentedByName { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}