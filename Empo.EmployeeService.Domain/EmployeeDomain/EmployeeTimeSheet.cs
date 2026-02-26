using Empo.BuildingBlocks.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Domain.EmployeeDomain;

public class EmployeeTimeSheet : DomainBase
{
    public DateOnly Date { get; set; }
    public TimeOnly ClockInTIme { get; set; }
    public TimeOnly ClockOutTIme { get; set; }

    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public EmployeeTimeSheet()
    {

    }
}
