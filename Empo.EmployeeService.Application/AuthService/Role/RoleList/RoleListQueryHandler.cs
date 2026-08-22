using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.AuthService.Intrfaces;

namespace Empo.EmployeeService.Application.AuthService.Role.RoleList;

public class RoleListQueryHandler : IQueryHandler<RoleListQuery, RoleListResponse>
{
    public IRoleService _service { get; }

    public RoleListQueryHandler(IRoleService service)
    {
        _service = service;
    }

    public async Task<RoleListResponse> Handle(RoleListQuery query, CancellationToken cancellationToken)
    {
        var response = await _service.ListAsync(query.request);
        return response;
    }
}
