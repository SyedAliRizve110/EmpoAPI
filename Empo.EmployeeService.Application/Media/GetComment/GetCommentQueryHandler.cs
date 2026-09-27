using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Media.CreatePost;

namespace Empo.EmployeeService.Application.Media.GetComment;

public class GetCommentQueryHandler: IQueryHandler<GetCommentQuery, List<CommentModel>>
{
    public IMediaService _service { get; }
    public GetCommentQueryHandler(IMediaService service)
    {
        _service = service;
    }
    public async Task<List<CommentModel>> Handle(GetCommentQuery query, CancellationToken cancellationToken)
    {
        return await _service.GetCommentsAsync(query._postId);
    }
}
