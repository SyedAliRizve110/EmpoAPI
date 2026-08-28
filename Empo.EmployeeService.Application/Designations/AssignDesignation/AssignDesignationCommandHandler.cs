using Empo.BuildingBlocks.Application.Configuration;
using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.EmployeeService.Application.Designations.ServiceInterface;
using Empo.EmployeeService.Application.Employees.ServiceInterface;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Designations.AssignDesignation;

public class AssignDesignationCommandHandler : ICommandHandler<AssignDesignationCommand, DesignationDto>
{
    public IDesignationService _service { get; }
    public IEmployeeService _empservice { get; }
    public AssignDesignationCommandHandler(IDesignationService service, IEmployeeService empservice)
    {
        _service = service;
        _empservice = empservice;
    }

    public async Task<DesignationDto> Handle(AssignDesignationCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;
        var designation = await _service.GetAsync(request.DesignationId);
        var employee = await _empservice.GetEmployeeDetails(request.EmployeeId);
        if (designation != null && employee != null)
        {
            var designationId = await _service.AssignDesignationAsync(request);
            return new DesignationDto { Id = designationId };
        }
        else throw new NotFoundException("Designation", request.DesignationId);
    }
}