using Empo.EmloyeeService.Api.Authorization;
using Empo.EmployeeService.Application.EmployeeWorkHistory;
using Empo.EmployeeService.Application.EmployeeWorkHistory.CreateEmployeeWorkHistory;
using Empo.EmployeeService.Application.EmployeeWorkHistory.GetEmployeeWorkHistory;
using Empo.EmployeeService.Application.EmployeeWorkHistory.UpdateEmployeeWorkHistory;
using Empo.EmployeeService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Empo.EmloyeeService.Api.Controllers;

[Route("api/employee-work-history")]
[ApiController]
[Authorize]
public class EmployeeWorkHistoryController : Controller
{
    private readonly IMediator _mediator;

    public EmployeeWorkHistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("")]
    [HttpPost]
    [HasPermission(Permissions.WorkHistoryCreate)]
    [ProducesResponseType(typeof(EmployeeWorkHistoryDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateEmployeeWorkHistory([FromBody] CreateEmployeWorkHistoryRequest request)
    {
        var dto = await _mediator.Send(CreateEmployeeWorkHistoryCommand.Create(request));
        return Ok(dto);
    }

    [Route("")]
    [HttpPut]
    [HasPermission(Permissions.WorkHistoryUpdate)]
    [ProducesResponseType(typeof(EmployeeWorkHistoryDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> UpdateEmployeeWorkHistory([FromBody] UpdateEmployeeWorkHistoryRequest request)
    {
        var dto = await _mediator.Send(UpdateEmployeeWorkHistoryCommand.Create(request));
        return Ok(dto);
    }

    [Route("{employeeId}")]
    [HttpGet]
    [HasPermission(Permissions.WorkHistoryGet)]
    [ProducesResponseType(typeof(List<EmployeeWorkHistoryModel>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetEmployeeWorkHistoryAsync([FromRoute] Guid employeeId)
    {
        var workHistory = await _mediator.Send(GetEmployeeWorkHistoryQuery.Create(employeeId));
        return Ok(workHistory);
    }
}
