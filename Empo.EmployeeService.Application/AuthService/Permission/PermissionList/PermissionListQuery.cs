using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.AuthService.Permission.PermissionList;

public class PermissionListQuery : IQuery<PermissionListResponse>
{
    public PermissionListRequest request { get; private set; }

    public PermissionListQuery()
    { }

    public static PermissionListQuery Get(PermissionListRequest request)
    {
        return new PermissionListQuery()
        {
            request = request
        };
    }
}
