using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Employees.EmployeService;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Employees.CreateEmployee;

public class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, EmployeeDto>
{
    public IEmployeeService _service { get; }
    public CreateEmployeeCommandHandler(IEmployeeService service)
    {
        _service = service;
    }

    public async Task<EmployeeDto> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var isExist = await this._service.IsEmployeeEmailExistsAsync(request.Email);
        if (!isExist)
        {
            var emp = await _service.AddEmployee(request);
            return new EmployeeDto { Id = emp.Id };
        }
        else
        {
            throw new Exception("Employee with this email already exists.");
        }
    }
}
