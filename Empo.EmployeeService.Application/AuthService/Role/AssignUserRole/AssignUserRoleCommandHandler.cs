using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.AuthService.Intrfaces;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.AuthService.Role.AssignUserRole;

public class AssignUserRoleCommandHandler : ICommandHandler<AssignUserRoleCommand, RoleDto>
{
    public IRoleService _service { get; }
    public AssignUserRoleCommandHandler(IRoleService service)
    {
        _service = service;
    }
    public async Task<RoleDto> Handle(AssignUserRoleCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var role = await this._service.GetAsync(request.RoleId);
        if (role != null)
        {
            var roleId = await _service.AssignUserRole(request);
            return new RoleDto { Id = roleId };
        }
        else
        {
            throw new Exception("Role with this name does not exists.");
        }
    }

}
