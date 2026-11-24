using Empo.EmployeeService.Application.Leaves.LeaveBalance.AdjustLeaveBalance;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Leaves.LeaveBalance;

public interface ILeaveBalanceService
{
    Task<List<LeaveBalanceModel>> GetByEmployeeIdAsync(Guid employeeId, int? year = null);
    Task<LeaveBalanceDto> AdjustAsync(AdjustLeaveBalanceRequest request);
}
