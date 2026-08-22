using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.AuthService.Role.CreateRole;

public class CreateRoleCommand : CommandBase<RoleDto>
{
    public CreateRoleRequest _request { get; set; }

    public static CreateRoleCommand Create(CreateRoleRequest request)
    {
        return new CreateRoleCommand
        {
            _request = request
        };
    }
}
