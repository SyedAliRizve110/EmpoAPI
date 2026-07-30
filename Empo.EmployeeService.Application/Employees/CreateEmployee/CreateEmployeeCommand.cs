using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;
using Empo.EmployeeService.Application.Employees.EmployeesModel;
namespace Empo.EmployeeService.Application.Employees.CreateEmployee;

public class CreateEmployeeCommand : CommandBase<EmployeeDto>
{
    public EmployeeModel _request { get; set; }
    public CreateEmployeeCommand()
    {

    }

    public static CreateEmployeeCommand Create(EmployeeModel request)
    {
        return new CreateEmployeeCommand
        {
            _request = request
        };
    }
}
