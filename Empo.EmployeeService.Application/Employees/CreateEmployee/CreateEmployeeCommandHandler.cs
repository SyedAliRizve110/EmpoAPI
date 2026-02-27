using AutoMapper;
using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Models;
using Empo.EmployeeService.Domain.EmployeeDomain;
using Empo.EmployeeService.Domain.OpsServiceInterfaces;

namespace Empo.EmployeeService.Application.Employees.CreateEmployee;

public class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, EmployeeDto>
{
    public IEmployeeOpsService _service { get; }
    public IMapper _mapper { get; }

    public CreateEmployeeCommandHandler(IEmployeeOpsService service, IMapper mapper)

    {
        _mapper = mapper;
        _service = service;
    }

    public async Task<EmployeeDto> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        Employee employee = _mapper.Map<Employee>(request);
        var employeeId = await this._service.Save(employee);
        return new EmployeeDto { Id = employeeId };
    }
}