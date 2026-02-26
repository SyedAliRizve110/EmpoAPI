using Empo.EmployeeService.Domain.Employee.Enums;
using Empo.EmployeeService.Domain.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Domain.EmployeeDomain;

public class Employee
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public DateOnly DateOfBirth { get; set; }
    public bool IsActive { get; set; }
    public EmployeeRoleEnum EmployeeRole { get; set; }

    public Guid PermanentAddressId { get; set; }
    [ForeignKey(nameof(PermanentAddressId))]
    public Address PermanentAddress { get; set; }
    public Guid TemporaryAddressId { get; set; }
    [ForeignKey(nameof(TemporaryAddressId))]
    public Address TemporaryAddress { get; set; }

    public ICollection<EmployeeTimeSheet> EmployeeTimeSheets { get; set; }
    public EmployeePhone Phone { get; set; }
}
