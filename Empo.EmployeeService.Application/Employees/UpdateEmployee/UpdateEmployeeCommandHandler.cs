using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Employees.ServiceInterface;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Employees.UpdateEmployee;

public class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand, EmployeeDto>
{
    public IEmployeeService _service { get; }
    public UpdateEmployeeCommandHandler(IEmployeeService service)
    {
        _service = service;
    }
    public async Task<EmployeeDto> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var emp = await this._service.GetEmployeeDetails(request.Id);
        if (emp.Id == request.Id)
        {
            var _emp = await _service.UpdateEmployee(request);
            return new EmployeeDto { Id = _emp };
        }
        else
        {
           throw new Exception("Employee with this email does not exists.");
        }
    }
}