namespace Empo.EmployeeService.Application.Branch.BranchEmployeeList;

public class BranchEmployeeListResponse
{
    public IEnumerable<BranchEmployeeModel> Collecion { get; set; }
    public long TotalRecords { get; set; }
}

public class BranchEmployeeModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
}
