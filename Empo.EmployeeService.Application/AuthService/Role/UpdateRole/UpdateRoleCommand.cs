using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.AuthService.Role.UpdateRole;

public class UpdateRoleCommand : CommandBase<RoleDto>
{
    public UpdateRoleRequest _request { get; set; }
    public static UpdateRoleCommand Update(UpdateRoleRequest request)
    {
        return new UpdateRoleCommand
        {
            _request = request
        };
    }
}
