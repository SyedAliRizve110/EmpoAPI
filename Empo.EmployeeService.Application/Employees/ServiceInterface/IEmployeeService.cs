using Empo.EmployeeService.Application.Employees.CreateEmployee;
using Empo.EmployeeService.Application.Employees.EmployeesModel;
using Empo.EmployeeService.Application.Employees.GetEmployeeList;

namespace Empo.EmployeeService.Application.Employees.ServiceInterface;

public interface IEmployeeService
{
    Task<Guid> AddEmployee(CreateEmployeeRequestModel request);
    Task<bool> IsEmployeeEmailExistsAsync(string email);
    Task<Guid> UpdateEmployee(EmployeeModel request);
    Task<EmployeeModel> GetEmployeeDetails(Guid employeeId);
    Task<GetEmployeeListResponse> EmployeeListAsync(GetEmployeeListRequest request);
}
