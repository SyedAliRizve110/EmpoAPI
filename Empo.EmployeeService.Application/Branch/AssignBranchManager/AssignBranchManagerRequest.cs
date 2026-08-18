namespace Empo.EmployeeService.Application.Branch.AssignBranchManager;

public class AssignBranchManagerRequest
{
    public Guid ManagerId { get; set; }
    public Guid BranchId { get; set; }
}
