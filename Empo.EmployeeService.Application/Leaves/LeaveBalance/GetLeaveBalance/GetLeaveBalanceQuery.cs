using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.Leaves.LeaveBalance.GetLeaveBalance;

public class GetLeaveBalanceQuery : IQuery<List<LeaveBalanceModel>>
{
    public Guid EmployeeId { get; private set; }
    public int? Year { get; private set; }

    public static GetLeaveBalanceQuery Create(Guid employeeId, int? year = null)
    {
        return new GetLeaveBalanceQuery
        {
            EmployeeId = employeeId,
            Year = year
        };
    }
}
