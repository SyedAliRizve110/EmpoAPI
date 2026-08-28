using Empo.BuildingBlocks.Application.Configuration;
using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.EmployeeService.Application.Department.ServiceInterface;
using Empo.EmployeeService.Application.Employees.ServiceInterface;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Department.AssignManager;

public class AssignManagerCommandHandler : ICommandHandler<AssignManagerCommand, DepartmentDto>
{
    public IDepartmentService _service { get; }
    public IEmployeeService _empservice { get; }
    public AssignManagerCommandHandler(IDepartmentService service, IEmployeeService empservice)
    {
        _service = service;
        _empservice = empservice;
    }

    public async Task<DepartmentDto> Handle(AssignManagerCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;
        var department = await _service.GetDepartmentDetails(request.DepartmentId);
        var employee = await _empservice.GetEmployeeDetails(request.ManagerId);
        if (department != null && employee != null)
        {
            var departmentId = await _service.AssignManagerAsync(request);
            return new DepartmentDto { Id = departmentId };
        }
        else throw new NotFoundException("Department", request.DepartmentId);
    }
}
