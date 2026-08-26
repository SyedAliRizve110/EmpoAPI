using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.EmployeeWorkHistory.CreateEmployeeWorkHistory;

public class CreateEmployeeWorkHistoryCommand : CommandBase<EmployeeWorkHistoryDto>
{
    public CreateEmployeWorkHistoryRequest _request { get; set; }

    public static CreateEmployeeWorkHistoryCommand Create(CreateEmployeWorkHistoryRequest request)
    {
        return new CreateEmployeeWorkHistoryCommand
        {
            _request = request
        };
    }
}
