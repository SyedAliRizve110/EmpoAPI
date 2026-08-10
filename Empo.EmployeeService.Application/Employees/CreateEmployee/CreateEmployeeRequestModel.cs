using Empo.EmployeeService.Application.Enums;
using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.Employees.CreateEmployee;

public class CreateEmployeeRequestModel
{
    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [MaxLength(200)]
    public string Email { get; set; }

    [Required(ErrorMessage = "DOB is required")]
    public DateOnly DateOfBirth { get; set; }

    [Required(ErrorMessage = "Employee Role is required")]
    public EmployeeRoleEnum EmployeeRole { get; set; }
    public CreateEmployeeAddressModel Address { get; set; }
    public CreateEmployeePhoneModel Phone { get; set; }
}

public class CreateEmployeePhoneModel
{
    [Required(ErrorMessage = "Country code is required")]
    [MaxLength(3)]
    public string CountryCode { get; set; }

    [Required(ErrorMessage = "Number is required")]
    [MaxLength(20)]
    public string Number { get; set; }
}

public class CreateEmployeeAddressModel
{
    [Required(ErrorMessage = "Address1 is required")]
    [MaxLength(256)]
    public string Address1 { get; set; }

    [MaxLength(256)]
    public string Address2 { get; set; }

    [Required(ErrorMessage = "City is required")]
    [MaxLength(60)]
    public string City { get; set; }

    [Required(ErrorMessage = "State is required")]
    [MaxLength(60)]
    public string State { get; set; }

    [Required(ErrorMessage = "Zip Code is required")]
    [MaxLength(6)]
    public string ZipCode { get; set; }

    [Required(ErrorMessage = "Country is required")]
    [MaxLength(100)]
    public string Country { get; set; }

}