using Empo.BuildingBlocks.Infrastructure.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Employee;

public class EmployeePhoneEntity : TenantEntityBase
{
    [MaxLength(3)]
    public string CountryCode { get; set; }

    [MaxLength(20)]
    public string Number { get; set; }

    [MaxLength(20)]
    public string Extension { get; set; }

    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; }
    public EmployeePhoneEntity()
    {
            
    }

}
