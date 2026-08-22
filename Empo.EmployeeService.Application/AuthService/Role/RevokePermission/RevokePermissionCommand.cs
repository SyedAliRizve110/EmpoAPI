using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.AuthService.Role.AssignRolePermission;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.AuthService.Role.RevokePermission;

public class RevokePermissionCommand : CommandBase<RoleDto>
{
    public RevokePermissionRequest _request { get; set; }
    public static RevokePermissionCommand Update(RevokePermissionRequest request)
    {
        return new RevokePermissionCommand
        {
            _request = request
        };
    }
}
