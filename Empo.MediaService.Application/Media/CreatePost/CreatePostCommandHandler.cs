using Empo.BuildingBlocks.Application.Configuration;

namespace Empo.MediaService.Application.Media.CreatePost;

public class CreatePostCommandHandler : ICommandHandler<CreatePostCommand, PostModel>
{
    public IMediaService _service { get; }
    public CreatePostCommandHandler(IMediaService service)
    {
        _service = service;
    }

    public async Task<PostModel> Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
        return await _service.CreatePostAsync(command._request, command._currentUserId);
    }
}
