using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.EmployeeEducation;

public class EmployeeEducationModel
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }

    [MaxLength(100)]
    public string Degree { get; set; }

    [MaxLength(150)]
    public string Institution { get; set; }

    [MaxLength(150)]
    public string FieldOfStudy { get; set; }

    public int StartYear { get; set; }
    public int? EndYear { get; set; }
    public decimal? Percentage { get; set; }
}
