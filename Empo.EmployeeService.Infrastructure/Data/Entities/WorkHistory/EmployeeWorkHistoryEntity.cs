using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Infrastructure.Data.Entities.Branch;
using Empo.EmployeeService.Infrastructure.Data.Entities.Department;
using Empo.EmployeeService.Infrastructure.Data.Entities.Designation;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.WorkHistory;

public class EmployeeWorkHistoryEntity : EntityBase
{
    [ForeignKey(nameof(DesignationId))]
    public Guid DesignationId { get; set; }
    public DesignationEntity Designation { get; set; }

    [ForeignKey(nameof(DepartmentId))]
    public Guid? DepartmentId { get; set; }
    public DepartmentEntity? Department { get; set; }

    [ForeignKey(nameof(BranchId))]
    public Guid? BranchId { get; set; }
    public BranchEntity? Branch { get; set; }

    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

    [MaxLength(500)]
    public string Remarks { get; set; }

    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; }
}
