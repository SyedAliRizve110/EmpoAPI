using Empo.BuildingBlocks.Application.Contracts;
using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.EmployeeService.Application.Employees.ServiceInterface;

namespace Empo.EmployeeService.Application.Leaves.LeaveBalance.GetLeaveBalance;

public class GetLeaveBalanceQueryHandler : IQueryHandler<GetLeaveBalanceQuery, List<LeaveBalanceModel>>
{
    private readonly ILeaveBalanceService _service;
    private readonly IEmployeeService _empService;

    public GetLeaveBalanceQueryHandler(ILeaveBalanceService service, IEmployeeService empService)
    {
        _service = service;
        _empService = empService;
    }

    public async Task<List<LeaveBalanceModel>> Handle(GetLeaveBalanceQuery query, CancellationToken cancellationToken)
    {
        var employee = await _empService.GetEmployeeDetails(query.EmployeeId);
        if (employee == null)
        {
            throw new NotFoundException("Employee", query.EmployeeId);
        }
        return await _service.GetByEmployeeIdAsync(query.EmployeeId, query.Year);
    }
}
