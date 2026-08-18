using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.Branch.GetBranch;

public class GetBranchQuery : IQuery<BranchModel>
{
    public Guid _branchId { get; private set; }
    public GetBranchQuery()
    {

    }

    public static GetBranchQuery Get(Guid BranchId)
    {
        return new GetBranchQuery()
        {
            _branchId = BranchId
        };
    }
}
