using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Department;

public class DepartmentEntity : EntityBase
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    [ForeignKey(nameof(ManagerId))]
    public Guid? ManagerId { get; set; }
    public ICollection<EmployeeEntity> Employees { get; set; }
}
