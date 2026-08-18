using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Employees.GetEmployeeList;

namespace Empo.EmployeeService.Application.Branch.ListBranch;

public class BranchListQuery : IQuery<BranchListResponse>
{
    public BranchListRequest request { get; private set; }

    public BranchListQuery()
    { }

    public static BranchListQuery Get(BranchListRequest request)
    {
        return new BranchListQuery()
        {
            request = request
        };
    }
}
