using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Leave;

public class EmployeeLeaveBalanceEntity : EntityBase
{
    public int Year { get; set; }
    public decimal TotalAllocated { get; set; }
    public decimal Used { get; set; }
    public decimal CarriedForward { get; set; }
    public decimal Available => TotalAllocated + CarriedForward - Used;

    [ForeignKey(nameof(EmployeeId))]
    public Guid EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; } = null!;

    [ForeignKey(nameof(LeaveTypeId))]
    public Guid LeaveTypeId { get; set; }
    public LeaveTypeEntity LeaveType { get; set; } = null!;
}
