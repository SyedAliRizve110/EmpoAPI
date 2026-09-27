using Empo.BuildingBlocks.Application.Configuration;

namespace Empo.EmployeeService.Application.Media.ToggleLike;

public class LikeCommandHandler : ICommandHandler<LikeCommand, bool>
{
    public IMediaService _service { get; }
    public LikeCommandHandler(IMediaService service)
    {
        _service = service;
    }
    public async Task<bool> Handle(LikeCommand command, CancellationToken cancellationToken)
    {
        return await _service.ToggleLikeAsync(command._postId, command._currentUserId);
    }
}
