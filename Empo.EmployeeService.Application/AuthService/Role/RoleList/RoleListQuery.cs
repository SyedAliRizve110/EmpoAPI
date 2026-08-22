using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Department.ListDepartment;

namespace Empo.EmployeeService.Application.AuthService.Role.RoleList;

public class RoleListQuery : IQuery<RoleListResponse>
{
    public RoleListRequest request { get; private set; }

    public RoleListQuery()
    { }

    public static RoleListQuery Get(RoleListRequest request)
    {
        return new RoleListQuery()
        {
            request = request
        };
    }
}
