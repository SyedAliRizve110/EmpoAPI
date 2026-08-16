using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Designations.ServiceInterface;

namespace Empo.EmployeeService.Application.Designations.DesignationList;

public class DesignationListQueryHandler : IQueryHandler<DesignationListQuery, DesignationListResponse>
{
    public IDesignationService _repo { get; }

    public DesignationListQueryHandler(IDesignationService repo)
    {
        _repo = repo;
    }

    public async Task<DesignationListResponse> Handle(DesignationListQuery query, CancellationToken cancellationToken)
    {
        var response = await _repo.ListAsync(query.request);
        return response;
    }
}
