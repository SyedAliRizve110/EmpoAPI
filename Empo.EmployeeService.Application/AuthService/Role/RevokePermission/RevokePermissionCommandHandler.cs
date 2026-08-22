using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.AuthService.Intrfaces;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.AuthService.Role.RevokePermission;

public class RevokePermissionCommandHandler : ICommandHandler<RevokePermissionCommand, RoleDto>
{
    public IRoleService _service { get; }
    public RevokePermissionCommandHandler(IRoleService service)
    {
        _service = service;
    }
    public async Task<RoleDto> Handle(RevokePermissionCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var role = await this._service.GetAsync(request.RoleId);
        if (role != null)
        {
            var roleId = await _service.RevokePermission(request);
            return new RoleDto { Id = roleId };
        }
        else
        {
            throw new Exception("Role with this name does not exists.");
        }
    }
}
