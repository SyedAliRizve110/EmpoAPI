using Empo.EmployeeService.Application.Media.CreatePost;

namespace Empo.EmployeeService.Application.Media.GetComment;

public class GetCommentQuery : QueryBase<List<CommentModel>>
{
    public Guid _postId { get; }
    private GetCommentQuery(Guid postId)
    {
        _postId = postId;
    }
    public static GetCommentQuery Create(Guid postId)
    {
        return new GetCommentQuery(postId);
    }
}
