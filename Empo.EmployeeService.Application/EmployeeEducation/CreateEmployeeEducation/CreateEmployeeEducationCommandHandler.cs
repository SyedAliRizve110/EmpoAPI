using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.EmployeeEducation.CreateEmployeeEducation;

public class CreateEmployeeEducationCommandHandler : ICommandHandler<CreateEmployeeEducationCommand, EmployeeEducationDto>
{
    public IEmployeeEducationService _service { get; }

    public CreateEmployeeEducationCommandHandler(IEmployeeEducationService service)
    {
        _service = service;
    }

    public async Task<EmployeeEducationDto> Handle(CreateEmployeeEducationCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var education = await _service.AddEmployeeEducation(request);
        return new EmployeeEducationDto { Id = education };
    }
}
