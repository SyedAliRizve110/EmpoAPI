using Empo.EmployeeService.Application.Media.CreatePost;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Media;

public interface IMediaService
{
    Task<PostModel> CreatePostAsync(CreatePostRequest request, Guid currentUserId);
    Task<GetFeedResponse> GetFeedAsync(GetFeedRequest request, Guid currentUserId);
    Task<bool> ToggleLikeAsync(Guid postId, Guid currentUserId);
    Task<CommentModel> AddCommentAsync(CreateCommentRequest request, Guid postId, Guid currentUserId);
    Task<List<CommentModel>> GetCommentsAsync(Guid postId);
    Task DeletePostAsync(Guid postId, Guid currentUserId, bool isModerator);
}
