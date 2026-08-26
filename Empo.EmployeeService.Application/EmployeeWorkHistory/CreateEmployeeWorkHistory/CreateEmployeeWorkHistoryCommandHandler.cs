using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.EmployeeWorkHistory.CreateEmployeeWorkHistory;

public class CreateEmployeeWorkHistoryCommandHandler : ICommandHandler<CreateEmployeeWorkHistoryCommand, EmployeeWorkHistoryDto>
{
    public IEmployeeWorkHistoryService _service { get; }

    public CreateEmployeeWorkHistoryCommandHandler(IEmployeeWorkHistoryService service)
    {
        _service = service;
    }

    public async Task<EmployeeWorkHistoryDto> Handle(CreateEmployeeWorkHistoryCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var workHistory = await _service.AddEmployeeWorkHistory(request);
        return new EmployeeWorkHistoryDto { Id = workHistory };
    }
}
