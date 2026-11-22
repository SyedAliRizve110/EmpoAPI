using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.Leaves.LeaveType.GetLeaveType;

public class GetLeaveTypeQueryHandler : IQueryHandler<GetLeaveTypeQuery, LeaveTypeRequestModel>
{
    public ILeaveTypeService _service { get; }
    public GetLeaveTypeQueryHandler(ILeaveTypeService service)
    {
        _service = service;
    }

    public async Task<LeaveTypeRequestModel> Handle(GetLeaveTypeQuery query, CancellationToken cancellationToken)
    {
        return await _service.GetAsyncById(query._leaveTypeId);
    }
}
