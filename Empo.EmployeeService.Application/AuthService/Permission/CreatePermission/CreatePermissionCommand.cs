using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.AuthService.Permission.CreatePermission;

public class CreatePermissionCommand : CommandBase<PermissionDto>
{
    public CreatePermissionRequest _request { get; set; }

    public static CreatePermissionCommand Create(CreatePermissionRequest request)
    {
        return new CreatePermissionCommand
        {
            _request = request
        };
    }
}
