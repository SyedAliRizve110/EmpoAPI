using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.AuthService.Intrfaces;

namespace Empo.EmployeeService.Application.AuthService.Role.GetRoleDetails;

public class GetRoleDetailQueryHandler : IQueryHandler<GetRoleDetailsQuery, GetRoleDetailResponse>
{
    public IRoleService _service { get; }

    public GetRoleDetailQueryHandler(IRoleService service)
    {
        _service = service;
    }

    public async Task<GetRoleDetailResponse> Handle(GetRoleDetailsQuery request, CancellationToken cancellationToken)
    {
        var role = await _service.GetAsync(request._roleId);
        return role;
    }
}
