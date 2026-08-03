using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Employees.ServiceInterface;

namespace Empo.EmployeeService.Application.Employees.GetEmployeeList;

public class GetEmployeeListQueryHandler : IQueryHandler<GetEmployeeListQuery, GetEmployeeListResponse>
{
    public IEmployeeService _repo { get; }

    public GetEmployeeListQueryHandler(IEmployeeService repo)
    {
            _repo = repo;
    }

    public async Task<GetEmployeeListResponse> Handle(GetEmployeeListQuery query, CancellationToken cancellationToken)
    {
        var response = await _repo.EmployeeListAsync(query.request);
        return response;
    }
}
