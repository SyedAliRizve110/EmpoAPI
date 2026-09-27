using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Models;
using MediatR;

namespace Empo.EmployeeService.Application.Media.DeletePost;

public class DeletePostCommandHandler : ICommandHandler<DeletePostCommand, bool>
{
    public IMediaService _service { get; }
    public DeletePostCommandHandler(IMediaService service)
    {
        _service = service;
    }
    public async Task<bool> Handle(DeletePostCommand command, CancellationToken cancellationToken)
    {
        await _service.DeletePostAsync(command._postId, command._currentUserId, command._isModerator);
        return true;
    }
}
