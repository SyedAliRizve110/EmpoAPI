using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;
using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Designation;

public class DesignationEntity : EntityBase
{
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(400)]
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public ICollection<EmployeeEntity> Employees { get; set; }
}
