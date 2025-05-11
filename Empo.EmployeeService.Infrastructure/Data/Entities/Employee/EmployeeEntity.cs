using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Domain.Employee.Enums;
using Empo.EmployeeService.Infrastructure.Data.Entities.CommonEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Employee;

public class EmployeeEntity : TenantEntityBase
{
    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    public string LastName { get; set; }

    [MaxLength(200)]
    public string Email { get; set; }

    public DateOnly DateOfBirth { get; set; }
    public bool IsActive { get; set; }
    public EmployeeRoleEnum EmployeeRole { get; set; }

    public Guid PermanentAddressId { get; set; }
    [ForeignKey(nameof(PermanentAddressId))]
    public AddressEntity PermanentAddress { get; set; }
    public Guid TemporaryAddressId { get; set; }
    [ForeignKey(nameof(TemporaryAddressId))]
    public AddressEntity TemporaryAddress { get; set; }

    public ICollection<EmployeeTimeSheetEntity> EmployeeTimeSheets { get; set; }
    public EmployeePhoneEntity Phone { get; set; }
}
