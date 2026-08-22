using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Department.ServiceInterface;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Department.UpdateDepartment;

public class UpdateDepartmentCommandHandler : ICommandHandler<UpdateDepartmentCommand, DepartmentDto>
{
    public IDepartmentService _service { get; }
    public UpdateDepartmentCommandHandler(IDepartmentService service)
    {
        _service = service;
    }
    public async Task<DepartmentDto> Handle(UpdateDepartmentCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var department = await this._service.GetDepartmentDetails(request.Id);
        if (department != null)
        {
            var _emp = await _service.UpdateDepartment(request);
            return new DepartmentDto { Id = _emp };
        }
        else
        {
            throw new Exception("Department with this name does not exists.");
        }
    }
}
