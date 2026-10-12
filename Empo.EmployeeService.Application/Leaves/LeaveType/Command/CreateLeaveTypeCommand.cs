using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Leaves.LeaveType.Command;

public class CreateLeaveTypeCommand : CommandBase<LeaveTypeDto>
{
    public CreateLeaveTypeRequest _request { get; set; }

    public static CreateLeaveTypeCommand Create(CreateLeaveTypeRequest request)
    {
        return new CreateLeaveTypeCommand
        {
            _request = request
        };
    }
}
