using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.Branch.BranchEmployeeList;

public class BranchEmployeeListQuery : IQuery<BranchEmployeeListResponse>
{
    public BranchEmployeeListRequest request { get; private set; }

    public BranchEmployeeListQuery()
    { }

    public static BranchEmployeeListQuery Get(BranchEmployeeListRequest request)
    {
        return new BranchEmployeeListQuery()
        {
            request = request
        };
    }
}
