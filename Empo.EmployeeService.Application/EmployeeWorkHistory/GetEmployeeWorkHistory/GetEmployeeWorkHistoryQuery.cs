using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.EmployeeWorkHistory.GetEmployeeWorkHistory;

public class GetEmployeeWorkHistoryQuery : IQuery<List<EmployeeWorkHistoryModel>>
{
    public Guid _employeeId { get; private set; }

    public GetEmployeeWorkHistoryQuery()
    {
    }

    public static GetEmployeeWorkHistoryQuery Create(Guid employeeId)
    {
        return new GetEmployeeWorkHistoryQuery()
        {
            _employeeId = employeeId
        };
    }
}
