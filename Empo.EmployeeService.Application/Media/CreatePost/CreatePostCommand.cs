using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Media.CreatePost;

public class CreatePostCommand : CommandBase<PostModel>
{
    public CreatePostRequest _request { get; set; } = null!;
    public Guid _currentUserId { get; set; }

    public static CreatePostCommand Create(CreatePostRequest request, Guid currentUserId)
    {
        return new CreatePostCommand
        {
            _request = request,
            _currentUserId = currentUserId,
        };
    }
}
