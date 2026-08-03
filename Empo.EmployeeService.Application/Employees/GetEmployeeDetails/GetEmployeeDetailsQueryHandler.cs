using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Employees.EmployeesModel;
using Empo.EmployeeService.Application.Employees.ServiceInterface;

namespace Empo.EmployeeService.Application.Employees.GetEmployeeDetails;

public class GetEmployeeDetailsQueryHandler : IQueryHandler<GetEmployeeDetailsQuery, EmployeeModel>
{
    public IEmployeeService _service { get; }

    public GetEmployeeDetailsQueryHandler(IEmployeeService service)
    {
        _service = service;
    }

    public async Task<EmployeeModel> Handle(GetEmployeeDetailsQuery request, CancellationToken cancellationToken)
    {
        var employee = await _service.GetEmployeeDetails(request._employeeId);
        return employee;
    }
}
