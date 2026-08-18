namespace Empo.EmployeeService.Application.Branch.ListBranch;

public class BranchListResponse
{
    public IEnumerable<BranchModel> Collecion { get; set; }
    public long TotalRecords { get; set; }
}
