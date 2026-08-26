using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.EmployeeWorkHistory.UpdateEmployeeWorkHistory;

public class UpdateEmployeeWorkHistoryCommand : CommandBase<EmployeeWorkHistoryDto>
{
    public UpdateEmployeeWorkHistoryRequest _request { get; set; }

    public static UpdateEmployeeWorkHistoryCommand Create(UpdateEmployeeWorkHistoryRequest request)
    {
        return new UpdateEmployeeWorkHistoryCommand
        {
            _request = request
        };
    }
}
