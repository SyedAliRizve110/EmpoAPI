using Empo.EmloyeeService.Api.Authorization;
using Empo.EmployeeService.Application.Leaves.LeaveBalance;
using Empo.EmployeeService.Application.Leaves.LeaveBalance.AdjustLeaveBalance;
using Empo.EmployeeService.Application.Leaves.LeaveBalance.GetLeaveBalance;
using Empo.EmployeeService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Empo.EmloyeeService.Api.Controllers;

[Route("api/leave-balance")]
[ApiController]
[Authorize]
public class LeaveBalanceController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveBalanceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("{employeeId}")]
    [HttpGet]
    [HasPermission(Permissions.LeaveBalanceGet)]
    [ProducesResponseType(typeof(List<LeaveBalanceModel>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetLeaveBalancesAsync([FromRoute] Guid employeeId, [FromQuery] int? year)
    {
        var balances = await _mediator.Send(GetLeaveBalanceQuery.Create(employeeId, year));
        return Ok(balances);
    }

    [Route("")]
    [Route("adjust")]
    [HttpPut]
    [HasPermission(Permissions.LeaveBalanceAdjust)]
    [ProducesResponseType(typeof(LeaveBalanceDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AdjustLeaveBalanceAsync([FromBody] AdjustLeaveBalanceRequest request)
    {
        var dto = await _mediator.Send(AdjustLeaveBalanceCommand.Create(request));
        return Ok(dto);
    }
}
