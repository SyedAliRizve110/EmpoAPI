using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Department.CreateDepartment;

public class CreateDepartmentCommand : CommandBase<DepartmentDto>
{
    public CreateDepartmentRequest _request { get; set; }

    public static CreateDepartmentCommand Create(CreateDepartmentRequest request)
    {
        return new CreateDepartmentCommand
        {
            _request = request
        };
    }
}
