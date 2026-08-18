using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.Attendence.ClockIn;

public class ClockInRequest
{
    [Required(ErrorMessage = "Id is required")]
    public Guid EmployeeId { get; set; }
    public string? ClockInIP { get; set; }
    public string? ClockInDevice { get; set; }
}
