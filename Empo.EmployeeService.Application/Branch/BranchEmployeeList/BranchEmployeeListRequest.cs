using Empo.BuildingBlocks.Application.SharedModels;

namespace Empo.EmployeeService.Application.Branch.BranchEmployeeList;

public class BranchEmployeeListRequest : ModelFilterBase
{
    public Guid BranchId { get; set; }
    public string search { get; set; }

}
