using Empo.EmloyeeService.Api.Authorization;
using Empo.EmployeeService.Application.AuthService.Permission.CreatePermission;
using Empo.EmployeeService.Application.AuthService.Permission.PermissionList;
using Empo.EmployeeService.Application.Branch.ListBranch;
using Empo.EmployeeService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Empo.EmloyeeService.Api.Controllers;

[Route("api/permission")]
[ApiController]
[Authorize]
public class PermissionController : Controller
{
    private readonly IMediator _mediator;
    public PermissionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("")]
    [HttpPost]
    [HasPermission(Permissions.PermissionCreate)]
    [ProducesResponseType(typeof(PermissionDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionRequest request)
    {
        var dto = await _mediator.Send(CreatePermissionCommand.Create(request));
        return Ok(dto);
    }

    [Route("list")]
    [HttpPost]
    [HasPermission(Permissions.PermissionList)]
    [ProducesResponseType(typeof(BranchListResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> PermissionListAsync([FromBody] PermissionListRequest request)
    {
        var permissionList = await _mediator.Send(PermissionListQuery.Get(request));
        return Ok(permissionList);
    }
}
