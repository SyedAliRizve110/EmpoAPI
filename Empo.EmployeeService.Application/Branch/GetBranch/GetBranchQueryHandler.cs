using Empo.BuildingBlocks.Application.Contracts;
using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.EmployeeService.Application.Branch.ServiceInterface;

namespace Empo.EmployeeService.Application.Branch.GetBranch;

public class GetBranchQueryHandler : IQueryHandler<GetBranchQuery, BranchModel>
{
    public IBranchService _service { get; }

    public GetBranchQueryHandler(IBranchService service)
    {
        _service = service;
    }

    public async Task<BranchModel> Handle(GetBranchQuery request, CancellationToken cancellationToken)
    {
        var branch = await _service.GetBranchDetails(request._branchId);
        if (branch == null)
        {
            throw new NotFoundException("Branch", request._branchId);
        }
        return branch;
    }
}

