using Empo.BuildingBlocks.Infrastructure.Configuration.Auth;
using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.MediaService.Api.Authorization;
using Empo.MediaService.Application.EmployeeDocument.DeleteDocument;
using Empo.MediaService.Application.EmployeeDocument.DownloadDocument;
using Empo.MediaService.Application.EmployeeDocument.GetDocument;
using Empo.MediaService.Application.EmployeeDocument.UploadDocument;
using Empo.MediaService.Application.EmployeeDocument.VerifyDocument;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Empo.MediaService.Api.Controllers;

[Route("api/employee/{employeeId}/document")]
[ApiController]
[Authorize]
public class EmployeeDocumentController : Controller
{
    private readonly IMediator _mediator;
    public EmployeeDocumentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub")
            ?? throw new UnauthorizedAccessException("User identity not found in token."));

    private Guid? OwnEmployeeId
    {
        get
        {
            var val = User.FindFirstValue("EmployeeId");
            return Guid.TryParse(val, out var id) ? id : null;
        }
    }

    private bool CanAccessEmployee(Guid employeeId) =>
        (OwnEmployeeId.HasValue && employeeId == OwnEmployeeId.Value) ||
        User.Claims.Any(c => c.Type == "permission" && c.Value == MediaPermissions.EmployeeDocumentManageAny);

    [Route("")]
    [HttpPost]
    [HasPermission(MediaPermissions.EmployeeDocumentUpload)]
    public async Task<IActionResult> Upload([FromRoute] Guid employeeId, [FromForm] UploadEmployeeDocumentRequest request)
    {
        if (!CanAccessEmployee(employeeId))
            throw new ForbiddenException("You can only upload documents for your own employee record.");

        var dto = await _mediator.Send(UploadEmployeeDocumentCommand.Create(request, employeeId, CurrentUserId));
        return Ok(dto);
    }

    [Route("")]
    [HttpGet]
    [HasPermission(MediaPermissions.EmployeeDocumentGet)]
    public async Task<IActionResult> List([FromRoute] Guid employeeId)
    {
        if (!CanAccessEmployee(employeeId))
            throw new ForbiddenException("You can only view your own documents.");

        var dto = await _mediator.Send(DocumentListQuery.Create(employeeId));
        return Ok(dto);
    }

    [Route("{documentId}/download")]
    [HttpGet]
    [HasPermission(MediaPermissions.EmployeeDocumentGet)]
    public async Task<IActionResult> Download([FromRoute] Guid employeeId, [FromRoute] Guid documentId)
    {
        if (!CanAccessEmployee(employeeId))
            throw new ForbiddenException("You can only download your own documents.");

        var file = await _mediator.Send(DownloadEmployeeDocumentQuery.Create(employeeId, documentId));
        return File(file.Stream, file.Contentype, file.FileName);
    }

    [Route("{documentId}")]
    [HttpDelete]
    [HasPermission(MediaPermissions.EmployeeDocumentDelete)]
    public async Task<IActionResult> Delete([FromRoute] Guid employeeId, [FromRoute] Guid documentId)
    {
        if (!CanAccessEmployee(employeeId))
            throw new ForbiddenException("You can only delete your own documents.");

        await _mediator.Send(DeleteEmployeeDocumentCommand.Create(employeeId, documentId));
        return NoContent();
    }

    [Route("{documentId}/verify")]
    [HttpPost]
    [HasPermission(MediaPermissions.EmployeeDocumentManageAny)]
    public async Task<IActionResult> Verify([FromRoute] Guid employeeId, [FromRoute] Guid documentId)
    {
        var dto = await _mediator.Send(VerifyEmployeeDocumentCommand.Create(employeeId, documentId, CurrentUserId));
        return Ok(dto);
    }
}

