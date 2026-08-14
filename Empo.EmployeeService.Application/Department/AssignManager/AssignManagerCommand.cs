using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Department.CreateDepartment;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Department.AssignManager;

public class AssignManagerCommand : CommandBase<DepartmentDto>
{
    public AssignManagerRequest request { get; set; }

    public AssignManagerCommand()
    {
    }

    public static AssignManagerCommand Create(AssignManagerRequest request)
    {
        return new AssignManagerCommand() { request = request };
    }
}
