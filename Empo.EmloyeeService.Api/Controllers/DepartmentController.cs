using Empo.EmloyeeService.Api.Authorization;
using Empo.EmployeeService.Application.Department.AddDepartmentEmployee;
using Empo.EmployeeService.Application.Department.AssignManager;
using Empo.EmployeeService.Application.Department.CreateDepartment;
using Empo.EmployeeService.Application.Department.DepartmentEmployeesList;
using Empo.EmployeeService.Application.Department.GetDepartmentDetails;
using Empo.EmployeeService.Application.Department.ListDepartment;
using Empo.EmployeeService.Application.Department.UpdateDepartment;
using Empo.EmployeeService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Empo.EmloyeeService.Api.Controllers;

[Route("api/department")]
[ApiController]
[Authorize]
public class DepartmentController : Controller
{

    private readonly IMediator _mediator;


    public DepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [Route("")]
    [HttpPost]
    [HasPermission(Permissions.DepartmentCreate)]
    [ProducesResponseType(typeof(DepartmentDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentRequest request)
    {
        var dto = await _mediator.Send(CreateDepartmentCommand.Create(request));
        return Ok(dto);
    }

    [Route("")]
    [HttpPut]
    [HasPermission(Permissions.DepartmentUpdate)]
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
    [HasPermission(Permissions.DepartmentGet)]
    [ProducesResponseType(typeof(GetDepartmentListResponse), (int)HttpStatusCode.OK)]

    public async Task<IActionResult> GetDepartmentListAsync([FromBody] GetDepartmentListRequest request)
    {
        var departmentList = await _mediator.Send(GetDepartmentListQuery.Get(request));
        return Ok(departmentList);
    }

    [Route("department-employee-list")]
    [HttpPost]
    [HasPermission(Permissions.DepartmentList)]
    [ProducesResponseType(typeof(GetDepartmentListResponse), (int)HttpStatusCode.OK)]

    public async Task<IActionResult> GetDepartmentEmployeeListAsync([FromBody] DepartmentEmployeeListRequest request)
    {
        var departmentEmployees = await _mediator.Send(DepartmentEmployeeListQuery.Get(request));
        return Ok(departmentEmployees);
    }


    [Route("assign-employee")]
    [HttpPost]
    [HasPermission(Permissions.DepartmentAssignEmoloyee)]
    [ProducesResponseType(typeof(DepartmentDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> AddDepartmentEmployee([FromBody] AddDepartmentEmployeeRequest request)
    {
        var dto = await _mediator.Send(AddDepartmentEmployeeCommand.Add(request));
        return Ok(dto);
    }

    [Route("assign-manager")]
    [HttpPost]
    [HasPermission(Permissions.DepartmentAssignManager)]
    [ProducesResponseType(typeof(DepartmentDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> AssignManager([FromBody] AssignManagerRequest request)
    {
        var dto = await _mediator.Send(AssignManagerCommand.Create(request));
        return Ok(dto);
    }
}
