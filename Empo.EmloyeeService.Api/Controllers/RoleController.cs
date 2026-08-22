using Empo.EmloyeeService.Api.Authorization;
using Empo.EmployeeService.Application.AuthService.Role.AssignRolePermission;
using Empo.EmployeeService.Application.AuthService.Role.AssignUserRole;
using Empo.EmployeeService.Application.AuthService.Role.CreateRole;
using Empo.EmployeeService.Application.AuthService.Role.GetRoleDetails;
using Empo.EmployeeService.Application.AuthService.Role.RevokePermission;
using Empo.EmployeeService.Application.AuthService.Role.RoleList;
using Empo.EmployeeService.Application.AuthService.Role.RoleStatus;
using Empo.EmployeeService.Application.AuthService.Role.UpdateRole;
using Empo.EmployeeService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Empo.EmloyeeService.Api.Controllers
{
    [Route("api/role")]
    [ApiController]
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IMediator _mediator;
        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("")]
        [HttpPost]
        [HasPermission(Permissions.RoleCreate)]
        [ProducesResponseType(typeof(RoleDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            var dto = await _mediator.Send(CreateRoleCommand.Create(request));
            return Ok(dto);
        }

        [Route("")]
        [HttpPut]
        [HasPermission(Permissions.RoleUpdate)]
        [ProducesResponseType(typeof(RoleDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequest request)
        {
            var dto = await _mediator.Send(UpdateRoleCommand.Update(request));
            return Ok(dto);
        }

        [Route("{roleId}")]
        [HttpGet]
        [HasPermission(Permissions.RoleGet)]
        [ProducesResponseType(typeof(RoleDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRoleDetailsAsync([FromRoute] Guid roleId)
        {
            var roleDetails = await _mediator.Send(GetRoleDetailsQuery.Get(roleId));
            return Ok(roleDetails);
        }

        [Route("list")]
        [HttpPost]
        [HasPermission(Permissions.RoleList)]
        [ProducesResponseType(typeof(RoleListResponse), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetRoleListAsync([FromBody] RoleListRequest request)
        {
            var roleList = await _mediator.Send(RoleListQuery.Get(request));
            return Ok(roleList);
        }

        [Route("status")]
        [HttpPost]
        [HasPermission(Permissions.RoleStatus)]
        [ProducesResponseType(typeof(RoleDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateStatus(RoleStatusRequest request)
        {
            var dto = await _mediator.Send(RoleStatusCommand.Create(request));
            return Ok(dto);
        }

        [Route("assign-permission")]
        [HttpPost]
        [HasPermission(Permissions.AssignPermission)]
        [ProducesResponseType(typeof(RoleDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AssignPermission(AssignRolePermissionRequest request)
        {
            var dto = await _mediator.Send(AssignRolePermisssionCommand.Update(request));
            return Ok(dto);
        }

        [Route("revoke-permission")]
        [HttpPost]
        [HasPermission(Permissions.RevokePermission)]
        [ProducesResponseType(typeof(RoleDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RevokePermission(RevokePermissionRequest request)
        {
            var dto = await _mediator.Send(RevokePermissionCommand.Update(request));
            return Ok(dto);
        }

        [Route("assign-user")]
        [HttpPost]
        [HasPermission(Permissions.AssignUser)]
        [ProducesResponseType(typeof(RoleDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AssignRoleToUser(AssignUserRoleRequest request)
        {
            var dto = await _mediator.Send(AssignUserRoleCommand.Update(request));
            return Ok(dto);
        }
    }
}
