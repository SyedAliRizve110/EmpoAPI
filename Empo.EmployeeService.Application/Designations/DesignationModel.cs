using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.Designations;

public class DesignationModel
{
    [Required(ErrorMessage = "ID is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }
    public bool IsActive { get; set; }
}
