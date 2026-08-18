using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Branch.ServiceInterface;

namespace Empo.EmployeeService.Application.Branch.BranchEmployeeList;

public class BranchEmployeeListQueryHandler : IQueryHandler<BranchEmployeeListQuery, BranchEmployeeListResponse>
{
    public IBranchService _repo { get; }

    public BranchEmployeeListQueryHandler(IBranchService repo)
    {
        _repo = repo;
    }

    public async Task<BranchEmployeeListResponse> Handle(BranchEmployeeListQuery query, CancellationToken cancellationToken)
    {
        var response = await _repo.BranchEmployeeListAsync(query.request);
        return response;
    }
}
