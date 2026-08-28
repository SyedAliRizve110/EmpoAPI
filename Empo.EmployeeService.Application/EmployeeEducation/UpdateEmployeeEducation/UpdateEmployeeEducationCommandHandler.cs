using Empo.BuildingBlocks.Application.Configuration;
using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.EmployeeEducation.UpdateEmployeeEducation
{
    public class UpdateEmployeeEducationCommandHandler : ICommandHandler<UpdateEmployeeEducationCommand, EmployeeEducationDto>
    {
        public IEmployeeEducationService _service { get; }

        public UpdateEmployeeEducationCommandHandler(IEmployeeEducationService service)
        {
            _service = service;
        }

        public async Task<EmployeeEducationDto> Handle(UpdateEmployeeEducationCommand command, CancellationToken cancellationToken)
        {
            var request = command._request;
            var existing = await _service.GetEmployeeEducationById(request.Id);
            if (existing == null)
            {
                throw new NotFoundException("Education", request.EmployeeId);
            }
            else
            {
                var updated = await _service.UpdateEmployeeEducation(request);
                return new EmployeeEducationDto { Id = updated };
            }
        }
    }
}
