using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.AuthService.Role.RoleStatus;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.AuthService.Role.RoleStatus;

public class RoleStatusCommand : CommandBase<RoleDto>
{
    public RoleStatusRequest _request { get; private set; }

    public RoleStatusCommand()
    { }

    public static RoleStatusCommand Create(RoleStatusRequest request)
    {
        return new RoleStatusCommand
        {
            _request = request
        };
    }
}
