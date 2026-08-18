using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.Department.CreateDepartment;

public class CreateDepartmentRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public Guid? ManagerId { get; set; }
}
