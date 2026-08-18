using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Branch.CreateBranch;

public class CreateBranchCommand : CommandBase<BranchDto>
{
    public CreateBranchRequest _request { get; set; }

    public static CreateBranchCommand Create(CreateBranchRequest request)
    {
        return new CreateBranchCommand
        {
            _request = request
        };
    }
}