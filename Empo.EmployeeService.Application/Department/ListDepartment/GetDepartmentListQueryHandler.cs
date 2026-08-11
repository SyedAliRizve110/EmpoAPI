using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Department.ServiceInterface;

namespace Empo.EmployeeService.Application.Department.ListDepartment;

public class GetDepartmentListQueryHandler : IQueryHandler<GetDepartmentListQuery, GetDepartmentListResponse>
{
    public IDepartmentService _repo { get; }

    public GetDepartmentListQueryHandler(IDepartmentService repo)
    {
        _repo = repo;
    }

    public async Task<GetDepartmentListResponse> Handle(GetDepartmentListQuery query, CancellationToken cancellationToken)
    {
        var response = await _repo.DepartmentListAsync(query.request);
        return response;
    }
}
