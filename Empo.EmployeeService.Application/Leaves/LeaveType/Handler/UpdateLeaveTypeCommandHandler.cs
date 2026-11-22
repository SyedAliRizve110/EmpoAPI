using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Leaves.LeaveType.Command;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Leaves.LeaveType.Handler;

public class UpdateLeaveTypeCommandHandler : ICommandHandler<UpdateLeaveTypeCommand, LeaveTypeDto>
{
    public ILeaveTypeService _service {  get; }
    public UpdateLeaveTypeCommandHandler(ILeaveTypeService service)
    {
        _service = service;
    }

    public async Task<LeaveTypeDto> Handle(UpdateLeaveTypeCommand command, CancellationToken cancellationToken)
    {
        return await _service.UpdateAsync(command._request);
    }
}
