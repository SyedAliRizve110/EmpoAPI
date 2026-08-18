using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.Attendence.ClockOut;

public class ClockOutRequest
{
    [Required(ErrorMessage = "Id is required")]
    public Guid EmployeeId { get; set; }
    public string? ClockOutIP { get; set; }
    public string? ClockOutDevice { get; set; }
}