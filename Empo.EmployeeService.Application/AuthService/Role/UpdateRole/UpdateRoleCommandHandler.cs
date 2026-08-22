using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.AuthService.Intrfaces;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.AuthService.Role.UpdateRole;

public class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand, RoleDto>
{
    public IRoleService _service { get; }
    public UpdateRoleCommandHandler(IRoleService service)
    {
        _service = service;
    }
    public async Task<RoleDto> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var role = await this._service.GetAsync(request.Id);
        if (role != null)
        {
            var _emp = await _service.UpdateAsync(request);
            return new RoleDto { Id = role.Id };
        }
        else
        {
            throw new Exception("Role with this name does not exists.");
        }
    }
}
