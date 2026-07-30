using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Employees.EmployeService;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Employees.CreateEmployee;

public class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, EmployeeDto>
{
    public IEmployeeService _service {  get; }
    public CreateEmployeeCommandHandler()
    {
            
    }

    public Task<EmployeeDto> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        return _service.AddEmployee(request);
    }
}
