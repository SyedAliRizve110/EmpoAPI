using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Leaves.LeaveType.Command;

public class UpdateLeaveTypeCommand : CommandBase<LeaveTypeDto>
{
    public LeaveTypeRequestModel _request { get; set; }

    public static UpdateLeaveTypeCommand Create(LeaveTypeRequestModel request)
    {
        return new UpdateLeaveTypeCommand
        {
            _request = request
        };
    }
}
