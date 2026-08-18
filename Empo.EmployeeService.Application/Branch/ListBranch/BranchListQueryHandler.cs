using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Branch.ServiceInterface;

namespace Empo.EmployeeService.Application.Branch.ListBranch;

public class BranchListQueryHandler : IQueryHandler<BranchListQuery, BranchListResponse>
{
    public IBranchService _repo { get; }

    public BranchListQueryHandler(IBranchService repo)
    {
        _repo = repo;
    }

    public async Task<BranchListResponse> Handle(BranchListQuery query, CancellationToken cancellationToken)
    {
        var response = await _repo.BranchListAsync(query.request);
        return response;
    }
}
