using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.EmployeeBank.Get;
public class GetEmployeeBankQuery : IQuery<EmployeeBankModel>
{
    public Guid _employeeId { get; private set; }

    public GetEmployeeBankQuery()
    {
    }

    public static GetEmployeeBankQuery Create(Guid employeeId)
    {
        return new GetEmployeeBankQuery()
        {
            _employeeId = employeeId
        };
    }
}

