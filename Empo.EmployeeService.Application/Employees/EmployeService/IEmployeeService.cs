using Empo.EmployeeService.Application.Employees.EmployeesModel;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Employees.EmployeService;

public interface IEmployeeService
{
    Task<EmployeeDto> AddEmployee(EmployeeModel request);
    Task<bool> IsEmployeeEmailExistsAsync(string email);    
}
