using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Department.AddDepartmentEmployee;

public class AddDepartmentEmployeeCommand : CommandBase<DepartmentDto>
{
    public AddDepartmentEmployeeRequest _request { get; private set; }
    public AddDepartmentEmployeeCommand()
    {

    }

    public static AddDepartmentEmployeeCommand Add(AddDepartmentEmployeeRequest request)
    {
        return new AddDepartmentEmployeeCommand
        {
            _request = request
        };
    }
}
