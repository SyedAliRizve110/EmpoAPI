using Empo.EmployeeService.Application.Department.AddDepartmentEmployee;
using Empo.EmployeeService.Application.Department.AssignManager;
using Empo.EmployeeService.Application.Department.CreateDepartment;
using Empo.EmployeeService.Application.Department.DepartmentEmployeesList;
using Empo.EmployeeService.Application.Department.GetDepartmentDetails;
using Empo.EmployeeService.Application.Department.ListDepartment;
using Empo.EmployeeService.Application.Department.UpdateDepartment;
using Empo.EmployeeService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Empo.EmloyeeService.Api.Controllers;

[Route("api/department")]
[ApiController]
public class DepartmentController : Controller
{

    private readonly IMediator _mediator;


    public DepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [Route("")]
    [HttpPost]
    [ProducesResponseType(typeof(DepartmentDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentRequest request)
    {
        var dto = await _mediator.Send(CreateDepartmentCommand.Create(request));
        return Ok(dto);
    }

    [Route("")]
    [HttpPut]
    [ProducesResponseType(typeof(DepartmentDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentRequestModel request)
    {
        var dto = await _mediator.Send(UpdateDepartmentCommand.Update(request));
        return Ok(dto);
    }

    [Route("{departmentId}")]
    [HttpGet]
    [ProducesResponseType(typeof(DepartmentDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetDepartmentDetailsAsync([FromRoute] Guid departmentId)
    {
        var departmentDetails = await _mediator.Send(GetDepartmentDetailsQuery.Get(departmentId));
        return Ok(departmentDetails);
    }

    [Route("list")]
    [HttpPost]
    [ProducesResponseType(typeof(GetDepartmentListResponse), (int)HttpStatusCode.OK)]

    public async Task<IActionResult> GetDepartmentListAsync([FromBody] GetDepartmentListRequest request)
    {
        var departmentList = await _mediator.Send(GetDepartmentListQuery.Get(request));
        return Ok(departmentList);
    }

    [Route("employeelist")]
    [HttpPost]
    [ProducesResponseType(typeof(GetDepartmentListResponse), (int)HttpStatusCode.OK)]

    public async Task<IActionResult> GetDepartmentEmployeeListAsync([FromBody] DepartmentEmployeeListRequest request)
    {
        var departmentEmployees = await _mediator.Send(DepartmentEmployeeListQuery.Get(request));
        return Ok(departmentEmployees);
    }


    [Route("addemployee")]
    [HttpPost]
    [ProducesResponseType(typeof(DepartmentDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> AddDepartmentEmployee([FromBody] AddDepartmentEmployeeRequest request)
    {
        var dto = await _mediator.Send(AddDepartmentEmployeeCommand.Add(request));
        return Ok(dto);
    }

    [Route("assignmanager")]
    [HttpPost]
    [ProducesResponseType(typeof(DepartmentDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> AssignManager([FromBody] AssignManagerRequest request)
    {
        var dto = await _mediator.Send(AssignManagerCommand.Create(request));
        return Ok(dto);
    }
}
