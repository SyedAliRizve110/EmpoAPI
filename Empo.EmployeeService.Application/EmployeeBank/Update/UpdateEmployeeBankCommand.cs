using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.EmployeeBank.Update;

public class UpdateEmployeeBankCommand : CommandBase<EmployeeBankDto>
{
    public EmployeeBankModel _request { get; set; }

    public static UpdateEmployeeBankCommand Create(EmployeeBankModel request)
    {
        return new UpdateEmployeeBankCommand
        {
            _request = request
        };
    }
}
