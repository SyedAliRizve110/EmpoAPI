namespace Empo.EmployeeService.Application.Branch.AssignBranchEmployee;

public class AssignBranchEmployeeRequest
{
    public List<Guid>? EmployeeId { get; set; }
    public Guid? ManagerId { get; set; }
    public Guid BranchId { get; set; }
}
