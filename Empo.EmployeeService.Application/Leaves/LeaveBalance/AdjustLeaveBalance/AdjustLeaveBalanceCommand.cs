using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Leaves.LeaveBalance.AdjustLeaveBalance;

public class AdjustLeaveBalanceCommand : CommandBase<LeaveBalanceDto>
{
    public AdjustLeaveBalanceRequest Request { get; set; }

    public static AdjustLeaveBalanceCommand Create(AdjustLeaveBalanceRequest request)
    {
        return new AdjustLeaveBalanceCommand
        {
            Request = request
        };
    }
}
