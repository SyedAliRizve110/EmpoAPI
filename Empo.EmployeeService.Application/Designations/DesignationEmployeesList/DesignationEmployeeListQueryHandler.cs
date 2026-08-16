using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Department.ServiceInterface;
using Empo.EmployeeService.Application.Designations.ServiceInterface;

namespace Empo.EmployeeService.Application.Designations.DesignationEmployeesList;

public class DesignationEmployeeListQueryHandler : IQueryHandler<DesignationEmployeeListQuery, DesignationEmployeeListResponse>
{
    public IDesignationService _repo { get; }

    public DesignationEmployeeListQueryHandler(IDesignationService repo)
    {
        _repo = repo;
    }

    public async Task<DesignationEmployeeListResponse> Handle(DesignationEmployeeListQuery query, CancellationToken cancellationToken)
    {
        var response = await _repo.DesignationEmployeeListAsync(query.request);
        return response;
    }
}
