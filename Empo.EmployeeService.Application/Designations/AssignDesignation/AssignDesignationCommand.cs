using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Designations.AssignDesignation;

public class AssignDesignationCommand : CommandBase<DesignationDto>
{
    public AssignDesignationRequest request { get; set; }

    public AssignDesignationCommand()
    {
    }

    public static AssignDesignationCommand Assign(AssignDesignationRequest request)
    {
        return new AssignDesignationCommand() { request = request };
    }
}
