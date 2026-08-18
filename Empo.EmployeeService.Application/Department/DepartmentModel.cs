using Empo.EmployeeService.Application.Employees.EmployeesModel;
using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.Department;

public class DepartmentModel
{
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Named is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public Guid ManagerId { get; set; }
}
