using Empo.BuildingBlocks.Infrastructure.Data;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Leave;

public class LeaveTypeEntity : EntityBase
{
    public string Name { get; set; }
    public int DefaultDaysPerYear { get; set; }
    public bool IsPaid { get; set; }
    public bool RequiresApproval { get; set; } = true;
    public bool CarryForwardAllowed { get; set; }
    public int MaxCarryForwardDays { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<EmployeeLeaveBalanceEntity> EmployeeLeaveBalances { get; set; }
    = new List<EmployeeLeaveBalanceEntity>();
}
