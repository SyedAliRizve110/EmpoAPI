using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.AuthService.Intrfaces;

namespace Empo.EmployeeService.Application.AuthService.Permission.PermissionList;

public class PermissionListQueryHandler : IQueryHandler<PermissionListQuery, PermissionListResponse>
{
    public IPermissionService _repo { get; }

    public PermissionListQueryHandler(IPermissionService repo)
    {
        _repo = repo;
    }

    public async Task<PermissionListResponse> Handle(PermissionListQuery query, CancellationToken cancellationToken)
    {
        var response = await _repo.ListAsync(query.request);
        return response;
    }
}
