using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Leaves.LeaveType.Command;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Leaves.LeaveType.Handler;

public class CreateLeaveTypeCommandHandler : ICommandHandler<CreateLeaveTypeCommand, LeaveTypeDto>
{
    public ILeaveTypeService _service { get; }
    public CreateLeaveTypeCommandHandler(ILeaveTypeService service)
    {
        _service = service;
    }
    public async Task<LeaveTypeDto> Handle(CreateLeaveTypeCommand command, CancellationToken cancellationToken)
    {
        return await _service.AddAsync(command._request);
    }
}
