using Empo.BuildingBlocks.Application.SharedModels;

namespace Empo.EmployeeService.Application.Branch.ListBranch;

public class BranchListRequest : ModelFilterBase
{
    public string search { get; set; }
}
