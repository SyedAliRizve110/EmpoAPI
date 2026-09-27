using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Media.CreatePost;

namespace Empo.EmployeeService.Application.Media.GetFeed;

public class GetFeedQueryHandler : IQueryHandler<GetFeedQuery, GetFeedResponse>
{
    public IMediaService _service { get; }
    public GetFeedQueryHandler(IMediaService service)
    {
        _service = service;
    }

    public async Task<GetFeedResponse> Handle(GetFeedQuery query, CancellationToken cancellationToken)
    {
        return await _service.GetFeedAsync(query._request, query._currentUserId);
    }
}
