using Empo.EmployeeService.Application.EmployeeBank.Create;

namespace Empo.EmployeeService.Application.EmployeeBank;

public interface IEmployeeBankService
{
    Task<Guid> AddEmployeeBank(CreateEmployeeBankRequest request);
    Task<bool> IsAccountNumberExistsAsync(string accountNumber);
    Task<Guid> UpdateEmployeeBank(EmployeeBankModel request);
    Task<EmployeeBankModel> GetEmployeeBank(Guid employeeId);
}
