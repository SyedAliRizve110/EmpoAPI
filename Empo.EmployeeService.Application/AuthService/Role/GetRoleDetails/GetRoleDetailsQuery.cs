using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.AuthService.Role.GetRoleDetails;

public class GetRoleDetailsQuery : IQuery<GetRoleDetailResponse>
{
    public Guid _roleId { get; private set; }

    public static GetRoleDetailsQuery Get(Guid roleId)
    {
        return new GetRoleDetailsQuery()
        {
            _roleId = roleId,
        };
    }
}
