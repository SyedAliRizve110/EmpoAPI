namespace Empo.EmployeeService.Application.Attendence.ClockIn;

public class ClockInRequest
{
    public Guid EmployeeId { get; set; }
    public string? ClockInIP { get; set; }
    public string? ClockInDevice { get; set; }
}
