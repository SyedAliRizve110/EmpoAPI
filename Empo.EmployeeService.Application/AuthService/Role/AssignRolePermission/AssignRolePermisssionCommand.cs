using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.AuthService.Role.AssignRolePermission;

public class AssignRolePermisssionCommand : CommandBase<RoleDto>
{
    public AssignRolePermissionRequest _request { get; set; }
    public static AssignRolePermisssionCommand Update(AssignRolePermissionRequest request)
    {
        return new AssignRolePermisssionCommand
        {
            _request = request
        };
    }
}
