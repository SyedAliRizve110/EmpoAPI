using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.MediaService.Application.Media.CreatePost;

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
