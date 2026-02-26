using Empo.EmployeeService.Application.Employees.CreateEmployee;
using Empo.EmployeeService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Empo.EmloyeeService.Api.Controllers;

[Route("api/employee")]
[ApiController]
public class EmployeeController : Controller
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
          _mediator = mediator;
    }

    [Route("")]
    [HttpPost]
    [ProducesResponseType(typeof(EmployeeDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest request)
    {
        var dto = await _mediator.Send(CreateEmployeeCommand.Create(request));
        return Ok(dto); 
    }
    }
