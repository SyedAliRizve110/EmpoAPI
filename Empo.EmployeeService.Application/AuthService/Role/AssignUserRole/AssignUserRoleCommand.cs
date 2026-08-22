using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.AuthService.Role.AssignUserRole;

public class AssignUserRoleCommand : CommandBase<RoleDto>
{
    public AssignUserRoleRequest _request { get; set; }
    public static AssignUserRoleCommand Update(AssignUserRoleRequest request)
    {
        return new AssignUserRoleCommand
        {
            _request = request
        };
    }
}
