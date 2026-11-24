using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.Leaves.LeaveBalance.AdjustLeaveBalance;

public class AdjustLeaveBalanceRequest
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "EmployeeId is required")]
    public Guid EmployeeId { get; set; }

    [Required(ErrorMessage = "LeaveTypeId is required")]
    public Guid LeaveTypeId { get; set; }

    public int Year { get; set; }

    public decimal? TotalAllocated { get; set; }

    public decimal? Used { get; set; }

    public decimal? CarriedForward { get; set; }

    public decimal? AdjustmentDays { get; set; }

    public string? Reason { get; set; }
}
