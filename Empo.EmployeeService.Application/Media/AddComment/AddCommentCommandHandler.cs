using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Media.CreatePost;

namespace Empo.EmployeeService.Application.Media.AddComment;

public class AddCommentCommandHandler : ICommandHandler<AddCommentCommand, CommentModel>
{
    public IMediaService _service { get; }
    public AddCommentCommandHandler(IMediaService service)
    {
        _service = service;
    }
    public async Task<CommentModel> Handle(AddCommentCommand command, CancellationToken cancellationToken)
    {
        return await _service.AddCommentAsync(command._request, command._postId, command._currentUserId);
    }
}
