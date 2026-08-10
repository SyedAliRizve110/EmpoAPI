using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Application.Enums;
using Empo.EmployeeService.Infrastructure.Data.Entities.CommonEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Employee;

public class EmployeeEntity : EntityBase
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
    public Guid AddressId { get; set; }
    [ForeignKey(nameof(AddressId))]
    public AddressEntity Address { get; set; }

    public ICollection<EmployeeAttendanceEntity> Attendance { get; set; }
    public EmployeePhoneEntity Phone { get; set; }
}
