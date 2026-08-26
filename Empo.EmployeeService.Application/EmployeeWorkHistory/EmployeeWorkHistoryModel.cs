using Empo.EmployeeService.Application.Branch;
using Empo.EmployeeService.Application.Department;
using Empo.EmployeeService.Application.Designations;
using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.EmployeeWorkHistory;

public class EmployeeWorkHistoryModel
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }

    public Guid DesignationId { get; set; }
    public DesignationModel Designation { get; set; }

    public Guid? DepartmentId { get; set; }
    public DepartmentModel Department { get; set; }

    public Guid? BranchId { get; set; }
    public BranchModel Branch { get; set; }

    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

    [MaxLength(500)]
    public string Remarks { get; set; }
}
