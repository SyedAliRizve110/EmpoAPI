using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Leaves.LeaveBalance.AdjustLeaveBalance;

public class AdjustLeaveBalanceCommandHandler : ICommandHandler<AdjustLeaveBalanceCommand, LeaveBalanceDto>
{
    private readonly ILeaveBalanceService _service;

    public AdjustLeaveBalanceCommandHandler(ILeaveBalanceService service)
    {
        _service = service;
    }

    public async Task<LeaveBalanceDto> Handle(AdjustLeaveBalanceCommand command, CancellationToken cancellationToken)
    {
        return await _service.AdjustAsync(command.Request);
    }
}
