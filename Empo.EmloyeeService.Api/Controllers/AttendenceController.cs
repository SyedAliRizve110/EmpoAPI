using Empo.EmloyeeService.Api.Authorization;
using Empo.EmployeeService.Application.Attendence.ActiveEmployeesList;
using Empo.EmployeeService.Application.Attendence.AttendenceList;
using Empo.EmployeeService.Application.Attendence.ClockIn;
using Empo.EmployeeService.Application.Attendence.ClockOut;
using Empo.EmployeeService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Empo.EmloyeeService.Api.Controllers;

[Route("api/attendence")]
[ApiController]
[Authorize]
public class AttendenceController : Controller
{
    private readonly IMediator _mediator;


    public AttendenceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("clockIn")]
    [HttpPost]
    [HasPermission(Permissions.AttendenceClockIn)]
    [ProducesResponseType(typeof(EmployeeDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> ClockIn([FromBody] ClockInRequest request)
    {
        var dto = await _mediator.Send(ClockInCommand.Create(request));
        return Ok();
    }

    [Route("clockOut")]
    [HttpPost]
    [HasPermission(Permissions.AttendenceClockOut)]
    [ProducesResponseType(typeof(EmployeeDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> ClockOut([FromBody] ClockOutRequest request)
    {
        var dto = await _mediator.Send(ClockOutCommand.Create(request));
        return Ok();
    }

    [Route("list")]
    [HttpPost]
    [HasPermission(Permissions.AttendenceList)]
    [ProducesResponseType(typeof(AttendanceListResponse), (int)HttpStatusCode.OK)]

    public async Task<IActionResult> GetAttendanceListAsync([FromBody] AttendanceListRequest request)
    {
        var attendanceList = await _mediator.Send(AttendanceListQuery.Create(request));
        return Ok(attendanceList);
    }


    [Route("active-employee")]
    [HttpPost]
    [HasPermission(Permissions.AttendenceActive)]
    [ProducesResponseType(typeof(ActiveEmployeeListResponse), (int)HttpStatusCode.OK)]

    public async Task<IActionResult> ActiveEmployeeListAsync([FromBody] ActiveEmployeeListRequest request)
    {
        var activeEmployees = await _mediator.Send(ActiveEmployeeListQuery.Create(request));
        return Ok(activeEmployees);
    }
}
