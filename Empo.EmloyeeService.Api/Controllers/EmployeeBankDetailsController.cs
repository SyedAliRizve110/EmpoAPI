using Empo.EmloyeeService.Api.Authorization;
using Empo.EmployeeService.Application.EmployeeBank;
using Empo.EmployeeService.Application.EmployeeBank.Create;
using Empo.EmployeeService.Application.EmployeeBank.Get;
using Empo.EmployeeService.Application.EmployeeBank.Update;
using Empo.EmployeeService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Empo.EmloyeeService.Api.Controllers;

[Route("api/bank")]
[ApiController]
[Authorize]
public class EmployeeBankController : Controller
{
    private readonly IMediator _mediator;

    public EmployeeBankController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("")]
    [HttpPost]
    [HasPermission(Permissions.EmployeeBankCreate)]
    [ProducesResponseType(typeof(EmployeeBankDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateEmployeeBank([FromBody] CreateEmployeeBankRequest request)
    {
        var dto = await _mediator.Send(CreateEmployeeBankCommand.Create(request));
        return Ok(dto);
    }

    [Route("")]
    [HttpPut]
    [HasPermission(Permissions.EmployeeBankUpdate)]
    [ProducesResponseType(typeof(EmployeeBankDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> UpdateEmployeeBank([FromBody] EmployeeBankModel request)
    {
        var dto = await _mediator.Send(UpdateEmployeeBankCommand.Create(request));
        return Ok(dto);
    }

    [Route("{employeeId}")]
    [HttpGet]
    [HasPermission(Permissions.EmployeeBankGet)]
    [ProducesResponseType(typeof(EmployeeBankModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetEmployeeBankAsync([FromRoute] Guid employeeId)
    {
        var bank = await _mediator.Send(GetEmployeeBankQuery.Create(employeeId));
        return Ok(bank);
    }
}
