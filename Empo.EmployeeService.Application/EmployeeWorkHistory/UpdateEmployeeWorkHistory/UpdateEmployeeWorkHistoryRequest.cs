using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.EmployeeWorkHistory.UpdateEmployeeWorkHistory;

public class UpdateEmployeeWorkHistoryRequest
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }

    public Guid DesignationId { get; set; }

    public Guid? DepartmentId { get; set; }

    public Guid? BranchId { get; set; }

    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

    [MaxLength(500)]
    public string Remarks { get; set; }
}
