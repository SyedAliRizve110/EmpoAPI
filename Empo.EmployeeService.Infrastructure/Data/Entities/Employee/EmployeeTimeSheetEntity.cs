using Empo.BuildingBlocks.Infrastructure.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Employee;

public class EmployeeTimeSheetEntity : TenantEntityBase
{
    public DateOnly Date { get; set; }
    public TimeOnly ClockInTIme { get; set; }
    public TimeOnly ClockOutTIme { get; set; }

    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; }
    public EmployeeTimeSheetEntity()
    {
            
    }
}
