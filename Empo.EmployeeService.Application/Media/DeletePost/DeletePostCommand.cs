using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Media.DeletePost;

public class DeletePostCommand : CommandBase<bool>
{
    public Guid _postId { get; set; }
    public Guid _currentUserId { get; set; }
    public bool _isModerator { get; set; }
    public DeletePostCommand(Guid postId, Guid currentUserId, bool isModerator)
    {
        _postId = postId;
        _currentUserId = currentUserId;
        _isModerator = isModerator;
    }

    public static DeletePostCommand Create(Guid postId, Guid currentUserId, bool isModerator)
    {
        return new DeletePostCommand(postId, currentUserId, isModerator);
    }
}
