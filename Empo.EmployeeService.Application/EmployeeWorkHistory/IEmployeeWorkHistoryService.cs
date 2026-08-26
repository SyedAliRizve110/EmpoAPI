using Empo.EmployeeService.Application.EmployeeWorkHistory.CreateEmployeeWorkHistory;
using Empo.EmployeeService.Application.EmployeeWorkHistory.UpdateEmployeeWorkHistory;

namespace Empo.EmployeeService.Application.EmployeeWorkHistory;

public interface IEmployeeWorkHistoryService
{
    Task<Guid> AddEmployeeWorkHistory(CreateEmployeWorkHistoryRequest request);
    Task<Guid> UpdateEmployeeWorkHistory(UpdateEmployeeWorkHistoryRequest request);
    Task<EmployeeWorkHistoryModel> GetEmployeeWorkHistoryById(Guid id);
    Task<List<EmployeeWorkHistoryModel>> GetEmployeeWorkHistoryList(Guid employeeId);
}
