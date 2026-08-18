using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Branch.AssignBranchManager;

public class AssignBranchManagerCommand : CommandBase<BranchDto>
{
    public AssignBranchManagerRequest request { get; set; }

    public AssignBranchManagerCommand()
    {
    }

    public static AssignBranchManagerCommand Create(AssignBranchManagerRequest request)
    {
        return new AssignBranchManagerCommand() { request = request };
    }
}
