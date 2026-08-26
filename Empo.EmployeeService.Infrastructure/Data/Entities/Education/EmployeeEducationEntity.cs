using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Education;

public class EmployeeEducationEntity : EntityBase
{
    [MaxLength(100)]
    public string Degree { get; set; }

    [MaxLength(150)]
    public string Institution { get; set; }

    [MaxLength(150)]
    public string FieldOfStudy { get; set; }

    public int StartYear { get; set; }
    public int? EndYear { get; set; }
    public decimal? Percentage { get; set; }

    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; }
}
