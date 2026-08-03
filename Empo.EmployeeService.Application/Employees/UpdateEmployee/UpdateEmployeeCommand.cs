using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Employees.CreateEmployee;
using Empo.EmployeeService.Application.Employees.EmployeesModel;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Employees.UpdateEmployee;

public class UpdateEmployeeCommand : CommandBase<EmployeeDto>
{
    public EmployeeModel _request { get; set; }

    public static UpdateEmployeeCommand Create(EmployeeModel request)
    {
        return new UpdateEmployeeCommand
        {
            _request = request
        };
    }
}
