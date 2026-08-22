using Empo.EmloyeeService.Api.Authorization;
using Empo.EmployeeService.Application.Designations;
using Empo.EmployeeService.Application.Designations.AssignDesignation;
using Empo.EmployeeService.Application.Designations.CreateDesignation;
using Empo.EmployeeService.Application.Designations.DesignationDetails;
using Empo.EmployeeService.Application.Designations.DesignationEmployeesList;
using Empo.EmployeeService.Application.Designations.DesignationList;
using Empo.EmployeeService.Application.Designations.UpdateDesignation;
using Empo.EmployeeService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Empo.EmloyeeService.Api.Controllers;

[Route("api/designation")]
[ApiController]
[Authorize]
public class DesignationController : Controller
{
    private readonly IMediator _mediator;
    public DesignationController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [Route("")]
    [HttpPost]
    [HasPermission(Permissions.DesignationCreate)]
    [ProducesResponseType(typeof(DesignationDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateDesignation([FromBody] CreateDesignationRequest request)
    {
        var dto = await _mediator.Send(CreateDesignationCommand.Create(request));
        return Ok(dto);
    }

    [Route("")]
    [HttpPut]
    [HasPermission(Permissions.DesignationUpdate)]
    [ProducesResponseType(typeof(DesignationDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> UpdateDesignation([FromBody] DesignationModel request)
    {
        var dto = await _mediator.Send(UpdateDesignationCommand.Update(request));
        return Ok(dto);
    }

    [Route("{designationId}")]
    [HttpGet]
    [HasPermission(Permissions.DesignationGet)]
    [ProducesResponseType(typeof(DesignationDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetDesignationDetailsAsync([FromRoute] Guid designationId)
    {
        var designationDetails = await _mediator.Send(GetDesignationDetailQuery.Get(designationId));
        return Ok(designationDetails);
    }

    [Route("list")]
    [HttpPost]
    [HasPermission(Permissions.DesignationList)]
    [ProducesResponseType(typeof(DesignationListResponse), (int)HttpStatusCode.OK)]

    public async Task<IActionResult> GetDesignationListAsync([FromBody] DesignationListRequest request)
    {
        var designationList = await _mediator.Send(DesignationListQuery.Get(request));
        return Ok(designationList);
    }

    [Route("employee-list")]
    [HttpPost]
    [HasPermission(Permissions.DesignationEmployeeList)]
    [ProducesResponseType(typeof(DesignationListResponse), (int)HttpStatusCode.OK)]

    public async Task<IActionResult> GetDesignationEmployeeListAsync([FromBody] DesignationEmployeeListRequest request)
    {
        var designationEmployees = await _mediator.Send(DesignationEmployeeListQuery.Get(request));
        return Ok(designationEmployees);
    }


    [Route("assign-designation")]
    [HttpPost]
    [HasPermission(Permissions.DesignationAssignDesignation)]
    [ProducesResponseType(typeof(DesignationDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> AssignDesignation([FromBody] AssignDesignationRequest request)
    {
        var dto = await _mediator.Send(AssignDesignationCommand.Assign(request));
        return Ok(dto);
    }
}
