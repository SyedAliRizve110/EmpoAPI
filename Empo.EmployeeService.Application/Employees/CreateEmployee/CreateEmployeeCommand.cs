using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Employees.CreateEmployee;

public class CreateEmployeeCommand : CommandBase<EmployeeDto>
{
    public CreateEmployeeRequest _request { get; private set; }
    public CreateEmployeeCommand() { }

    public static CreateEmployeeCommand Create(CreateEmployeeRequest request)
    {
        return new CreateEmployeeCommand
        {
            _request = request
        };

    }
}