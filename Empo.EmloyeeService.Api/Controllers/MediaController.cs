using Azure.Core;
using Empo.EmloyeeService.Api.Authorization;
using Empo.EmployeeService.Application.Media.AddComment;
using Empo.EmployeeService.Application.Media.CreatePost;
using Empo.EmployeeService.Application.Media.DeletePost;
using Empo.EmployeeService.Application.Media.GetComment;
using Empo.EmployeeService.Application.Media.GetFeed;
using Empo.EmployeeService.Application.Media.ToggleLike;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Empo.EmloyeeService.Api.Controllers;

[Route("api/post")]
[ApiController]
[Authorize]
public class MediaController : Controller
{
    private readonly IMediator _mediator;
    public MediaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub")!);
    private bool IsModerator =>
        User.Claims.Any(c => c.Type == "permission" && c.Value == Permissions.PostModerate);

    [Route("")]
    [HttpPost]
    [Consumes("multipart/form-data")]
    [HasPermission(Permissions.PostCreate)]
    [ProducesResponseType(typeof(PostMediaModel), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreatePost([FromForm] CreatePostRequest request)
    {
        var dto = await _mediator.Send(CreatePostCommand.Create(request, CurrentUserId));
        return Ok(dto);
    }

    [Route("feed")]
    [HttpGet]
    [HasPermission(Permissions.PostView)]
    [ProducesResponseType(typeof(GetFeedResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetFeed([FromQuery] GetFeedRequest request)
    {
        var dto = await _mediator.Send(GetFeedQuery.Create(request, CurrentUserId));
        return Ok(dto);
    }

    [Route("{postId}/like")]
    [HttpPost]
    [HasPermission(Permissions.PostLike)]
    public async Task<IActionResult> ToggleLike([FromRoute] Guid postId)
    {
        var liked = await _mediator.Send(LikeCommand.Create(postId, CurrentUserId));
        return Ok(new { liked });
    }

    [Route("{postId}/comment")]
    [HttpPost]
    [HasPermission(Permissions.PostComment)]
    [ProducesResponseType(typeof(CommentModel), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> AddComment([FromRoute] Guid postId, [FromBody] CreateCommentRequest request)
    {
        var dto = await _mediator.Send(AddCommentCommand.Create(request, postId, CurrentUserId));
        return Ok(dto);
    }

    [Route("{postId}/comments")]
    [HttpGet]
    [HasPermission(Permissions.PostView)]
    [ProducesResponseType(typeof(List<CommentModel>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetComments([FromRoute] Guid postId)
    {
        var dto = await _mediator.Send(GetCommentQuery.Create(postId));
        return Ok(dto);
    }

    [Route("{postId}")]
    [HttpDelete]
    [HasPermission(Permissions.PostDelete)]
    public async Task<IActionResult> DeletePost([FromRoute] Guid postId)
    {
        await _mediator.Send(DeletePostCommand.Create(postId, CurrentUserId, IsModerator));
        return NoContent();
    }
}
