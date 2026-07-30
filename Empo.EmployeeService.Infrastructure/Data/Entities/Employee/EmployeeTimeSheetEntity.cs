using Empo.BuildingBlocks.Infrastructure.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Employee;

public class EmployeeTimeSheetEntity : EntityBase
{
    public DateOnly Date { get; set; }
    public TimeOnly ClockInTime { get; set; }
    public TimeOnly ClockOutTime { get; set; }

    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; }
    public EmployeeTimeSheetEntity()
    {
            
    }
}
