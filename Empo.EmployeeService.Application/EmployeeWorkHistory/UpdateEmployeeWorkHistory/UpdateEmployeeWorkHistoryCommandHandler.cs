using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.EmployeeWorkHistory.UpdateEmployeeWorkHistory;

public class UpdateEmployeeWorkHistoryCommandHandler : ICommandHandler<UpdateEmployeeWorkHistoryCommand, EmployeeWorkHistoryDto>
{
    public IEmployeeWorkHistoryService _service { get; }

    public UpdateEmployeeWorkHistoryCommandHandler(IEmployeeWorkHistoryService service)
    {
        _service = service;
    }

    public async Task<EmployeeWorkHistoryDto> Handle(UpdateEmployeeWorkHistoryCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var existing = await _service.GetEmployeeWorkHistoryById(request.Id);
        if (existing == null)
        {
            throw new Exception("Work history record not found.");
        }
        else
        {
            var updated = await _service.UpdateEmployeeWorkHistory(request);
            return new EmployeeWorkHistoryDto { Id = updated };
        }
    }
}
