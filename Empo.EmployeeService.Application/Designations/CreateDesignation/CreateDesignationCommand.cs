using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Designations.CreateDesignation;

public class CreateDesignationCommand : CommandBase<DesignationDto>
{
    public CreateDesignationRequest _request { get; set; }

    public static CreateDesignationCommand Create(CreateDesignationRequest request)
    {
        return new CreateDesignationCommand
        {
            _request = request
        };
    }
}
