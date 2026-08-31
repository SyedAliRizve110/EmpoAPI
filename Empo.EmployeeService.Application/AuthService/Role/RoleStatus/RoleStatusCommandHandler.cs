using Empo.BuildingBlocks.Application.Configuration;
using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.EmployeeService.Application.AuthService.Intrfaces;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.AuthService.Role.RoleStatus;

public class RoleStatusCommandHandler : ICommandHandler<RoleStatusCommand, RoleDto>
{
    public IRoleService _service { get; }
    public RoleStatusCommandHandler(IRoleService service)
    {
        _service = service;
    }
    public async Task<RoleDto> Handle(RoleStatusCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var role = await this._service.GetAsync(request.Id);
        if (role != null)
        {
            var hasAssigned = await _service.HasRoleAssigned(role.Id);
            if (hasAssigned) throw new ConflictException("This role is still assigned to one or more users and cannot be deactivated.");

            var roleId = await _service.UpdateStatusAsync(request);
            return new RoleDto { Id = roleId };
        }
        else { throw new NotFoundException("Role", "Id", request.Id); }
    }
}
