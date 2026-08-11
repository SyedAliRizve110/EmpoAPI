using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Department.ServiceInterface;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Department.CreateDepartment;

public class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommand, DepartmentDto>
{
    public IDepartmentService _service { get; }
    public CreateDepartmentCommandHandler(IDepartmentService service)
    {
        _service = service;
    }

    public async Task<DepartmentDto> Handle(CreateDepartmentCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var departmentId = await _service.AddDepartment(request);
        return new DepartmentDto { Id = departmentId };
    }
}
