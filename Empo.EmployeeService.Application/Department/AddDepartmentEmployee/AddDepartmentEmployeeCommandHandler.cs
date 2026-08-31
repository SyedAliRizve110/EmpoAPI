using Empo.BuildingBlocks.Application.Configuration;
using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.EmployeeService.Application.Department.CreateDepartment;
using Empo.EmployeeService.Application.Department.ServiceInterface;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Department.AddDepartmentEmployee;

public class AddDepartmentEmployeeCommandHandler : ICommandHandler<AddDepartmentEmployeeCommand, DepartmentDto>
{
    public IDepartmentService _service { get; }
    public AddDepartmentEmployeeCommandHandler(IDepartmentService service)
    {
        _service = service;
    }

    public async Task<DepartmentDto> Handle(AddDepartmentEmployeeCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var department = await _service.GetDepartmentDetails(request.DepartmentId);
        if (department == null) { throw new NotFoundException("Department", request.DepartmentId); }
        var departmentId = await _service.AddDepartmentEmployeeAsync(request);
        return new DepartmentDto { Id = departmentId };
    }
}
