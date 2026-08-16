using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;
namespace Empo.EmployeeService.Application.Employees.CreateEmployee;

public class CreateEmployeeCommand : CommandBase<EmployeeDto>
{
    public CreateEmployeeRequestModel _request { get; set; }

    public static CreateEmployeeCommand Create(CreateEmployeeRequestModel request)
    {
        return new CreateEmployeeCommand
        {
            _request = request
        };
    }
}
