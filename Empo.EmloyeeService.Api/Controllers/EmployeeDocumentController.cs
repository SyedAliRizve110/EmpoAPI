using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.EmloyeeService.Api.Authorization;
using Empo.EmployeeService.Application.EmployeeDocument.DeleteDocument;
using Empo.EmployeeService.Application.EmployeeDocument.DownloadDocument;
using Empo.EmployeeService.Application.EmployeeDocument.GetDocument;
using Empo.EmployeeService.Application.EmployeeDocument.UploadDocument;
using Empo.EmployeeService.Application.EmployeeDocument.VerifyDocument;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Empo.EmloyeeService.Api.Controllers;

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

    private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub")!);

    private Guid OwnEmployeeId => Guid.Parse(User.FindFirstValue("EmployeeId")!);

    private bool CanAccessEmployee(Guid employeeId) =>
    employeeId == OwnEmployeeId ||
    User.Claims.Any(c => c.Type == "permission" && c.Value == Permissions.EmployeeDocumentManageAny);

    [Route("")]
    [HttpPost]
    [HasPermission(Permissions.EmployeeDocumentUpload)]
    public async Task<IActionResult> Upload([FromRoute] Guid employeeId, [FromForm] UploadEmployeeDocumentRequest request)
    {
        if (!CanAccessEmployee(employeeId))
            throw new ForbiddenException("You can only upload documents for your own employee record.");

        var dto = await _mediator.Send(UploadEmployeeDocumentCommand.Create(request, employeeId, CurrentUserId));
        return Ok(dto);
    }

    [Route("")]
    [HttpGet]
    [HasPermission(Permissions.EmployeeDocumentGet)]
    public async Task<IActionResult> List([FromRoute] Guid employeeId)
    {
        if (!CanAccessEmployee(employeeId))
            throw new ForbiddenException("You can only view your own documents.");

        var dto = await _mediator.Send(DocumentListQuery.Create(employeeId));
        return Ok(dto);
    }

    [Route("{documentId}/download")]
    [HttpGet]
    [HasPermission(Permissions.EmployeeDocumentGet)]
    public async Task<IActionResult> Download([FromRoute] Guid employeeId, [FromRoute] Guid documentId)
    {
        if (!CanAccessEmployee(employeeId))
            throw new ForbiddenException("You can only download your own documents.");

        var file = await _mediator.Send(DownloadEmployeeDocumentQuery.Create(employeeId, documentId));
        return File(file.Stream, file.Contentype, file.FileName);
    }

    [Route("{documentId}")]
    [HttpDelete]
    [HasPermission(Permissions.EmployeeDocumentDelete)]
    public async Task<IActionResult> Delete([FromRoute] Guid employeeId, [FromRoute] Guid documentId)
    {
        if (!CanAccessEmployee(employeeId))
            throw new ForbiddenException("You can only delete your own documents.");

        await _mediator.Send(DeleteEmployeeDocumentCommand.Create(employeeId, documentId));
        return NoContent();
    }

    [Route("{documentId}/verify")]
    [HttpPost]
    [HasPermission(Permissions.EmployeeDocumentManageAny)]
    public async Task<IActionResult> Verify([FromRoute] Guid employeeId, [FromRoute] Guid documentId)
    {
        var dto = await _mediator.Send(VerifyEmployeeDocumentCommand.Create(employeeId, documentId, CurrentUserId));
        return Ok(dto);
    }
}

