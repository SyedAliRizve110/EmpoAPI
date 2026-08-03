using Empo.EmployeeService.Application.CommonRequestModel;
using Empo.EmployeeService.Application.Employees.Enums;
using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.Employees.EmployeesModel;

public class EmployeeModel
{
    public Guid Id { get; set; }
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
    public AddressModel Address { get; set; }
    public ICollection<EmployeeTimeSheetModel> EmployeeTimeSheets { get; set; }
    public EmployeePhoneModel Phone { get; set; }
}
