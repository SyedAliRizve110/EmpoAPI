using Empo.EmployeeService.Application.CommonRequestModel;
using Empo.EmployeeService.Application.Employees.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Application.Employees.EmployeesModel;

public class EmployeeModel
{
    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    public string LastName { get; set; }

    [MaxLength(200)]
    public string Email { get; set; }

    public DateOnly DateOfBirth { get; set; }
    public EmployeeRoleEnum EmployeeRole { get; set; }
    public AddressModel Address { get; set; }
    public EmployeePhoneModel Phone { get; set; }
}
