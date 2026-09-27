using Empo.EmployeeService.Application.Media.CreatePost;

namespace Empo.EmployeeService.Application.Media.GetFeed;

public class GetFeedQuery : QueryBase<GetFeedResponse>
{
    public GetFeedRequest _request { get; set; }
    public Guid _currentUserId { get; }
    private GetFeedQuery(GetFeedRequest request, Guid currentUserId)
    {
        _request = request;
        _currentUserId = currentUserId;
    }
    public static GetFeedQuery Create(GetFeedRequest request, Guid currentUserId)
    {
        return new GetFeedQuery(request, currentUserId);
    }
}
