using Empo.EmployeeService.Application.Employees.CreateEmployee;
using Empo.EmployeeService.Application.Employees.EmployeesModel;

namespace Empo.EmployeeService.Application.Employees.EmployeService;

public interface IEmployeeService
{
    Task<Guid> AddEmployee(CreateEmployeeRequestModel request);
    Task<bool> IsEmployeeEmailExistsAsync(string email);
    Task<Guid> UpdateEmployee(EmployeeModel request);
}
