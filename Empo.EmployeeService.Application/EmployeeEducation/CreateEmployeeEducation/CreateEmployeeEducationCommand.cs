using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.EmployeeEducation.CreateEmployeeEducation;

public class CreateEmployeeEducationCommand : CommandBase<EmployeeEducationDto>
{
    public CreateEducationRequestModel _request { get; set; }

    public static CreateEmployeeEducationCommand Create(CreateEducationRequestModel request)
    {
        return new CreateEmployeeEducationCommand
        {
            _request = request
        };
    }
}
