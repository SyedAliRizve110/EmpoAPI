using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Department;

public class DepartmentEntity : EntityBase
{
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(400)]
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    [ForeignKey(nameof(ManagerId))]
    public Guid? ManagerId { get; set; }
    public ICollection<EmployeeEntity>? Employees { get; set; }
}
