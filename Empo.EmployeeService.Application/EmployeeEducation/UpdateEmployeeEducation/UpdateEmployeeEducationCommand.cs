using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.EmployeeEducation.UpdateEmployeeEducation;

public class UpdateEmployeeEducationCommand : CommandBase<EmployeeEducationDto>
{
    public EmployeeEducationModel _request { get; set; }

    public static UpdateEmployeeEducationCommand Create(EmployeeEducationModel request)
    {
        return new UpdateEmployeeEducationCommand
        {
            _request = request
        };
    }
}
