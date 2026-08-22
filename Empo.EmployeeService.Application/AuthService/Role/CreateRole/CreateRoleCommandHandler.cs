using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.AuthService.Intrfaces;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.AuthService.Role.CreateRole;

public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, RoleDto>
{
    public IRoleService _service { get; }
    public CreateRoleCommandHandler(IRoleService service)
    {
        _service = service;
    }

    public async Task<RoleDto> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;

        var roleId = await _service.AddAsync(request);
        return new RoleDto { Id = roleId };
    }
}
