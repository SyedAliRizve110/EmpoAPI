using Empo.EmloyeeService.Api.Authorization;
using Empo.EmployeeService.Application.Employees.CreateEmployee;
using Empo.EmployeeService.Application.Employees.EmployeesModel;
using Empo.EmployeeService.Application.Employees.GetEmployeeDetails;
using Empo.EmployeeService.Application.Employees.GetEmployeeList;
using Empo.EmployeeService.Application.Employees.UpdateEmployee;
using Empo.EmployeeService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Empo.EmloyeeService.Api.Controllers;

[Route("api/employee")]
[ApiController]
[Authorize] //must be logged in (valid JWT) for every action below
public class EmployeeController : Controller
{
    private readonly IMediator _mediator;


    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("")]
    [HttpPost]
    [HasPermission(Permissions.EmployeeCreate)] //any autheticated user with Employee.Create (eg. admin)
    [ProducesResponseType(typeof(EmployeeDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequestModel request)
    {
        var dto = await _mediator.Send(CreateEmployeeCommand.Create(request));
        return Ok(dto);
    }

    [Route("")]
    [HttpPut]
    [HasPermission(Permissions.EmployeeUpdate)] //anyone with Employee.Update
    [ProducesResponseType(typeof(EmployeeDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeModel request)
    {
        var dto = await _mediator.Send(UpdateEmployeeCommand.Create(request));
        return Ok(dto);
    }

    [Route("{employeeId}")]
    [HttpGet]
    [HasPermission(Permissions.EmployeeGet)] // anyone with Employee.Get
    [ProducesResponseType(typeof(EmployeeModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetEmployeeDetailsAsync([FromRoute] Guid employeeId)
    {
        var vendorDetails = await _mediator.Send(GetEmployeeDetailsQuery.Create(employeeId));
        return Ok(vendorDetails);
    }

    [Route("list")]
    [HttpPost]
    [HasPermission(Permissions.EmployeeList)] //anyone with Employee.List
    [ProducesResponseType(typeof(GetEmployeeListResponse), (int)HttpStatusCode.OK)]

    public async Task<IActionResult> GetEmployeeListAsync([FromBody] GetEmployeeListRequest request)
    {
        var employeeList = await _mediator.Send(GetEmployeeListQuery.Create(request));
        return Ok(employeeList);
    }
}
