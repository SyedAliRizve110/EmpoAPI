using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Branch.UpdateBranch;

public class UpdateBranchCommand : CommandBase<BranchDto>
{
    public BranchModel _request { get; set; }

    public static UpdateBranchCommand Update(BranchModel request)
    {
        return new UpdateBranchCommand
        {
            _request = request
        };
    }
}
