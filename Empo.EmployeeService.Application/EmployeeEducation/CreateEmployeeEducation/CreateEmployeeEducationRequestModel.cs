using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.EmployeeEducation.CreateEmployeeEducation;


public class CreateEducationRequestModel
{
    public List<CreateEmployeeEducationModel> Education {  get; set; }
}
public class CreateEmployeeEducationModel
{
    [Required(ErrorMessage = "Employee Id is required")]
    public Guid EmployeeId { get; set; }

    [Required(ErrorMessage = "Degree is required")]
    [MaxLength(100)]
    public string Degree { get; set; }

    [MaxLength(150)]
    public string Institution { get; set; }

    [MaxLength(150)]
    public string FieldOfStudy { get; set; }

    [Required(ErrorMessage = "Start Year is required")]
    public int StartYear { get; set; }

    public int? EndYear { get; set; }

    public decimal? Percentage { get; set; }
}
