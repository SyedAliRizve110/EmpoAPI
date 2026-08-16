using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Designations.ServiceInterface;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Designations.CreateDesignation;

public class CreateDesignationCommandHandler : ICommandHandler<CreateDesignationCommand, DesignationDto>
{
    public IDesignationService _service { get; }
    public CreateDesignationCommandHandler(IDesignationService service)
    {
        _service = service;
    }

    public async Task<DesignationDto> Handle(CreateDesignationCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;

        var designationId = await _service.AddAsync(request);
        return new DesignationDto { Id = designationId };
    }
}
