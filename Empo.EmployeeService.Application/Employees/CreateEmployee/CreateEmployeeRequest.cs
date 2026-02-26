using Empo.EmployeeService.Domain.Employee.Enums;
using Empo.EmployeeService.Domain.EmployeeDomain;
using Empo.EmployeeService.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.Employees.CreateEmployee;

public class CreateEmployeeRequest
{
    [Required(ErrorMessage = "Employee Name is required")]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    [Required(ErrorMessage = "Employee Name is required")]
    public string LastName { get; set; }

    [MaxLength(200)]
    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Date of Birth is required")]
    public DateOnly DateOfBirth { get; set; }
    public EmployeeRoleEnum EmployeeRole { get; set; }

    public Address PermanentAddress { get; set; }
    public Address TemporaryAddress { get; set; }

    public ICollection<EmployeeTimeSheet> EmployeeTimeSheets { get; set; }
    public EmployeePhone Phone { get; set; }
}
