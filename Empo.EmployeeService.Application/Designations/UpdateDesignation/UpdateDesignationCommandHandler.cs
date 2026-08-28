using Empo.BuildingBlocks.Application.Configuration;
using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.EmployeeService.Application.Designations.ServiceInterface;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Designations.UpdateDesignation;

public class UpdateDesignationCommandHandler : ICommandHandler<UpdateDesignationCommand, DesignationDto>
{
    public IDesignationService _service { get; }
    public UpdateDesignationCommandHandler(IDesignationService service)
    {
        _service = service;
    }

    public async Task<DesignationDto> Handle(UpdateDesignationCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var designation = await this._service.GetAsync(request.Id);
        if (designation != null)
        {
            var _designation = await _service.UpdateAsync(request);
            return new DesignationDto { Id = _designation };
        }
        else
        {
            throw new NotFoundException("Employee", request.Id);
        }
    }
}
