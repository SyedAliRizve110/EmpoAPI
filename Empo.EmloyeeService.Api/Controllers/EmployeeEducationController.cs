using Empo.EmloyeeService.Api.Authorization;
using Empo.EmployeeService.Application.EmployeeEducation;
using Empo.EmployeeService.Application.EmployeeEducation.CreateEmployeeEducation;
using Empo.EmployeeService.Application.EmployeeEducation.GetEmployeeEducation;
using Empo.EmployeeService.Application.EmployeeEducation.UpdateEmployeeEducation;
using Empo.EmployeeService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Empo.EmloyeeService.Api.Controllers;

[Route("api/education")]
[ApiController]
[Authorize]
public class EmployeeEducationController : Controller
{
    private readonly IMediator _mediator;

    public EmployeeEducationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("")]
    [HttpPost]
    [HasPermission(Permissions.EmployeeEducationCreate)]
    [ProducesResponseType(typeof(EmployeeEducationDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateEmployeeEducation([FromBody] CreateEducationRequestModel request)
    {
        var dto = await _mediator.Send(CreateEmployeeEducationCommand.Create(request));
        return Ok(dto);
    }

    [Route("")]
    [HttpPut]
    [HasPermission(Permissions.EmployeeEducationUpdate)]
    [ProducesResponseType(typeof(EmployeeEducationDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> UpdateEmployeeEducation([FromBody] EmployeeEducationModel request)
    {
        var dto = await _mediator.Send(UpdateEmployeeEducationCommand.Create(request));
        return Ok(dto);
    }

    [Route("{employeeId}")]
    [HttpGet]
    [HasPermission(Permissions.EmployeeEducationGet)]
    [ProducesResponseType(typeof(List<EmployeeEducationModel>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetEmployeeEducationAsync([FromRoute] Guid employeeId)
    {
        var education = await _mediator.Send(GetEmployeeEducationQuery.Create(employeeId));
        return Ok(education);
    }
}
