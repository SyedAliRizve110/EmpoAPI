using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.AuthService.Intrfaces;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.AuthService.Role.AssignRolePermission;

public class AssignRolePermisssionCommandHandler : ICommandHandler<AssignRolePermisssionCommand, RoleDto>
{
    public IRoleService _service { get; }
    public AssignRolePermisssionCommandHandler(IRoleService service)
    {
        _service = service;
    }
    public async Task<RoleDto> Handle(AssignRolePermisssionCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var role = await this._service.GetAsync(request.RoleId);
        if (role != null)
        {
            var roleId = await _service.AssignPermission(request);
            return new RoleDto { Id = roleId };
        }
        else
        {
            throw new Exception("Role with this name does not exists.");
        }
    }
}
