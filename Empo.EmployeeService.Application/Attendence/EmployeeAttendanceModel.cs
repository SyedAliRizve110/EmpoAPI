using Empo.EmployeeService.Application.Enums;

namespace Empo.EmployeeService.Application.Attendence;

public class EmployeeAttendanceModel
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly ClockInTime { get; set; }
    public TimeOnly ClockOutTime { get; set; }
    public double WorkedHours { get; set; }
    public AttendanceSessionStatus Status { get; set; }
    public string? ClockInIP { get; set; }
    public string? ClockOutIP { get; set; }
    public string? ClockInDevice { get; set; }
    public string? ClockOutDevice { get; set; }
}
