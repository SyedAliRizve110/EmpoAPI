using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.EmployeeWorkHistory.CreateEmployeeWorkHistory;

public class CreateEmployeWorkHistoryRequest
{
    public List<CreateEmployeeWorkHistoryModel> WorkHistory { get; set; }
}
public class CreateEmployeeWorkHistoryModel
{
    [Required(ErrorMessage = "Employee Id is required")]
    public Guid EmployeeId { get; set; }

    [Required(ErrorMessage = "Designation is required")]
    public Guid DesignationId { get; set; }

    public Guid? DepartmentId { get; set; }
    public Guid? BranchId { get; set; }

    [Required(ErrorMessage = "Start Date is required")]
    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    [MaxLength(500)]
    public string Remarks { get; set; }
}
