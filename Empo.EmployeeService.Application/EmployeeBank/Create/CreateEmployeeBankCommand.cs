using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.EmployeeBank.Create;

public class CreateEmployeeBankCommand : CommandBase<EmployeeBankDto>
{
    public CreateEmployeeBankRequest _request { get; set; }

    public static CreateEmployeeBankCommand Create(CreateEmployeeBankRequest request)
    {
        return new CreateEmployeeBankCommand
        {
            _request = request
        };
    }
}
