using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Application.Employees.EmployeesModel;

public class EmployeePhoneModel
{
    [MaxLength(3)]
    public string CountryCode { get; set; }

    [MaxLength(20)]
    public string Number { get; set; }
}
