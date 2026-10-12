using Empo.EmloyeeService.Api.Authorization;
using Empo.EmployeeService.Application.Leaves.LeaveType;
using Empo.EmployeeService.Application.Leaves.LeaveType.Command;
using Empo.EmployeeService.Application.Leaves.LeaveType.Query;
using Empo.EmployeeService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Empo.EmloyeeService.Api.Controllers;

[Route("api/leave-type")]
[ApiController]
[Authorize]
public class LeaveTypeController : ControllerBase
{
    public IMediator _mediator { get; }
    public LeaveTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [Route("")]
    [HttpPost]
    [HasPermission(Permissions.LeaveTypeManage)]
    [ProducesResponseType(typeof(LeaveTypeDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateLeaveType([FromBody] CreateLeaveTypeRequest request)
    {
        var dto = await _mediator.Send(CreateLeaveTypeCommand.Create(request));
        return Ok(dto);
    }

    [Route("")]
    [HttpPut]
    [HasPermission(Permissions.LeaveTypeManage)]
    [ProducesResponseType(typeof(LeaveTypeDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateLeaveType([FromBody] LeaveTypeRequestModel request)
    {
        var dto = await _mediator.Send(UpdateLeaveTypeCommand.Create(request));
        return Ok(dto);
    }

    [Route("{id}")]
    [HttpGet]
    [HasPermission(Permissions.LeaveTypeGet)]
    [ProducesResponseType(typeof(LeaveTypeDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetLeaveType([FromRoute] Guid id)
    {
        var dto = await _mediator.Send(GetLeaveTypeQuery.Create(id));
        return Ok(dto);
    }

    [Route("list")]
    [HttpPost]
    [HasPermission(Permissions.LeaveTypeGet)]
    [ProducesResponseType(typeof(GetLeaveTypeListResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetLeaveTypeList([FromBody] GetLeaveTypeListRequest request)
    {
        var dto = await _mediator.Send(GetLeaveTypeListQuery.Create(request));
        return Ok(dto);
    }
}
