using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Branch.AssignBranchEmployee;

public class AssignBranchEmployeeCommand : CommandBase<BranchDto>
{
    public AssignBranchEmployeeRequest request { get; set; }

    public AssignBranchEmployeeCommand()
    {
    }

    public static AssignBranchEmployeeCommand Create(AssignBranchEmployeeRequest request)
    {
        return new AssignBranchEmployeeCommand() { request = request };
    }
}
