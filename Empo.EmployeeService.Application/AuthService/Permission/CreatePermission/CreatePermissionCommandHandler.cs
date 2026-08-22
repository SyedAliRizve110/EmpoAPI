using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.AuthService.Intrfaces;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.AuthService.Permission.CreatePermission;

public class CreatePermissionCommandHandler : ICommandHandler<CreatePermissionCommand, PermissionDto>
{
    public IPermissionService _service { get; }
    public CreatePermissionCommandHandler(IPermissionService service)
    {
        _service = service;
    }

    public async Task<PermissionDto> Handle(CreatePermissionCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;

        var permissionId = await _service.AddAsync(request);
        return new PermissionDto { Id = permissionId };
    }
}
