namespace Empo.EmployeeService.Application.Leaves.LeaveBalance;

public class LeaveBalanceModel
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public Guid LeaveTypeId { get; set; }
    public string LeaveTypeName { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal TotalAllocated { get; set; }
    public decimal Used { get; set; }
    public decimal CarriedForward { get; set; }
    public decimal Available { get; set; }
}
