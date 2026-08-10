namespace Empo.EmployeeService.Application.Attendence.ClockOut;

public class ClockOutRequest
{
    public Guid EmployeeId { get; set; }
    public string? ClockOutIP { get; set; }
    public string? ClockOutDevice { get; set; }
}