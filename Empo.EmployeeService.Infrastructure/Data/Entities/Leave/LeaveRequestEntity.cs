using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Application.Enums;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Leave;

public class LeaveRequestEntity : EntityBase
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal NumberOfDays { get; set; }
    public string? Reason { get; set; }
    public LeaveRequestStatus Status { get; set; } = LeaveRequestStatus.Pending;
    public DateTime AppliedAt { get; set; }
    public Guid? ApprovedByUserId { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public string? RejectionReason { get; set; }

    [ForeignKey(nameof(EmployeeId))]
    public Guid EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; } = null!;

    [ForeignKey(nameof(LeaveTypeEntity))]
    public Guid LeaveTypeId { get; set; }
    public LeaveTypeEntity LeaveType { get; set; } = null!;
}
