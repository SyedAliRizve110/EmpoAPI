
using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Department.UpdateDepartment;

public class UpdateDepartmentCommand : CommandBase<DepartmentDto>
{
    public UpdateDepartmentRequestModel _request { get; set; }
    public static UpdateDepartmentCommand Update(UpdateDepartmentRequestModel request)
    {
        return new UpdateDepartmentCommand
        {
            _request = request
        };
    }
}
