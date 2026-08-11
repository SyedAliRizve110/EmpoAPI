using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Department.ServiceInterface;

namespace Empo.EmployeeService.Application.Department.DepartmentEmployeesList;

public class DepartmentEmployeeListQueryHandler : IQueryHandler<DepartmentEmployeeListQuery, DepartmentEmployeeListResponse>
{
    public IDepartmentService _repo { get; }

    public DepartmentEmployeeListQueryHandler(IDepartmentService repo)
    {
        _repo = repo;
    }

    public async Task<DepartmentEmployeeListResponse> Handle(DepartmentEmployeeListQuery query, CancellationToken cancellationToken)
    {
       var response = await _repo.DepartmentEmployeeListAsync(query.request);
        return response;
    }
}