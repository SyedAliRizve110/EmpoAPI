using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Media.CreatePost;

namespace Empo.EmployeeService.Application.Media.AddComment;

public class AddCommentCommand : CommandBase<CommentModel>
{
    public CreateCommentRequest _request { get; set; }
    public Guid _postId { get; set; }
    public Guid _currentUserId { get; set; }

    public AddCommentCommand(CreateCommentRequest request, Guid postId, Guid currentUserId)
    {
        _request = request;
        _postId = postId;
        _currentUserId = currentUserId;
    }

    public static AddCommentCommand Create(CreateCommentRequest request, Guid postId, Guid currentUserId)
    {
        return new AddCommentCommand(request, postId, currentUserId);
    }
}
