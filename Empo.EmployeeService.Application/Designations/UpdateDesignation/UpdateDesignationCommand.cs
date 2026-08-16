using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Designations.UpdateDesignation;

public class UpdateDesignationCommand : CommandBase<DesignationDto>
{
    public DesignationModel _request { get; set; }

    public static UpdateDesignationCommand Update(DesignationModel request)
    {
        return new UpdateDesignationCommand
        {
            _request = request
        };
    }
}
