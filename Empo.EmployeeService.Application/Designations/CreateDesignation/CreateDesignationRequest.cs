using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.Designations.CreateDesignation;

public class CreateDesignationRequest
{
    [Required(ErrorMessage = "Named is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }
    public bool IsActive { get; set; }
}
