using Empo.EmloyeeService.Api.Authorization;
using Empo.EmployeeService.Application.Branch;
using Empo.EmployeeService.Application.Branch.AssignBranchEmployee;
using Empo.EmployeeService.Application.Branch.AssignBranchManager;
using Empo.EmployeeService.Application.Branch.BranchEmployeeList;
using Empo.EmployeeService.Application.Branch.CreateBranch;
using Empo.EmployeeService.Application.Branch.GetBranch;
using Empo.EmployeeService.Application.Branch.ListBranch;
using Empo.EmployeeService.Application.Branch.UpdateBranch;
using Empo.EmployeeService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Empo.EmloyeeService.Api.Controllers;

[Route("api/branch")]
[ApiController]
[Authorize]
public class BranchController : ControllerBase
{
    private readonly IMediator _mediator;
    public BranchController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [Route("")]
    [HttpPost]
    [HasPermission(Permissions.BranchCreate)]
    [ProducesResponseType(typeof(BranchDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateBranch([FromBody] CreateBranchRequest request)
    {
        var dto = await _mediator.Send(CreateBranchCommand.Create(request));
        return Ok(dto);
    }

    [Route("")]
    [HttpPut]
    [HasPermission(Permissions.BranchUpdate)]
    [ProducesResponseType(typeof(BranchDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> UpdateBranch([FromBody] BranchModel request)
    {
        var dto = await _mediator.Send(UpdateBranchCommand.Update(request));
        return Ok(dto);
    }

    [Route("{branchId}")]
    [HttpGet]
    [HasPermission(Permissions.BranchGet)]
    [ProducesResponseType(typeof(BranchDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBranchDetailsAsync([FromRoute] Guid branchId)
    {
        var branchDetails = await _mediator.Send(GetBranchQuery.Get(branchId));
        return Ok(branchDetails);
    }

    [Route("list")]
    [HttpPost]
    [HasPermission(Permissions.BranchList)]
    [ProducesResponseType(typeof(BranchListResponse), (int)HttpStatusCode.OK)]

    public async Task<IActionResult> BranchListAsync([FromBody] BranchListRequest request)
    {
        var branchList = await _mediator.Send(BranchListQuery.Get(request));
        return Ok(branchList);
    }

    [Route("employee-list")]
    [HttpPost]
    [HasPermission(Permissions.BranchEmployeeList)]
    [ProducesResponseType(typeof(BranchEmployeeListResponse), (int)HttpStatusCode.OK)]

    public async Task<IActionResult> BranchEmployeeListAsync([FromBody] BranchEmployeeListRequest request)
    {
        var branchemployees = await _mediator.Send(BranchEmployeeListQuery.Get(request));
        return Ok(branchemployees);
    }


    [Route("assign-employee")]
    [HttpPost]
    [HasPermission(Permissions.BranchAssignEmoloyee)]
    [ProducesResponseType(typeof(BranchDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> AssignBranchEmployee([FromBody] AssignBranchEmployeeRequest request)
    {
        var dto = await _mediator.Send(AssignBranchEmployeeCommand.Create(request));
        return Ok(dto);
    }

    [Route("assign-manager")]
    [HttpPost]
    [HasPermission(Permissions.BranchAssignManager)]
    [ProducesResponseType(typeof(BranchDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> AssignBrnchManager([FromBody] AssignBranchManagerRequest request)
    {
        var dto = await _mediator.Send(AssignBranchManagerCommand.Create(request));
        return Ok(dto);
    }
}
