using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Employees.EmployeesModel;

namespace Empo.EmployeeService.Application.Employees.GetEmployeeDetails;

public class GetEmployeeDetailsQuery : IQuery<EmployeeModel>
{
    public Guid _employeeId { get; private set; }
    public GetEmployeeDetailsQuery()
    {
            
    }

    public static GetEmployeeDetailsQuery Create(Guid EmployeeId)
    {
        return new GetEmployeeDetailsQuery()
        {
            _employeeId = EmployeeId
        };
    }
}
