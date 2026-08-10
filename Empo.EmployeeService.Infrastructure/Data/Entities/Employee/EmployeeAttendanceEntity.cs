using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Application.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Employee;

public class EmployeeAttendanceEntity : EntityBase
{
    public DateOnly Date { get; set; }
    public TimeOnly ClockInTime { get; set; }
    public TimeOnly ClockOutTime { get; set; }
    public double WorkedHours { get; set; }
    public AttendanceSessionStatus Status { get; set; }
    public string? ClockInIP { get; set; }
    public string? ClockOutIP { get; set; }
    public string? ClockInDevice { get; set; }
    public string? ClockOutDevice { get; set; }

    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; }
    public EmployeeAttendanceEntity()
    {

    }
}