using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.Media.ToggleLike;

public class LikeCommand :CommandBase<bool>
{
    public Guid _postId { get; set; }
    public Guid _currentUserId { get; set;  }

    public static LikeCommand Create(Guid postId, Guid currentUserId)
    {
        return new LikeCommand
        {
            _postId = postId,
            _currentUserId = currentUserId
        };
    }
}
