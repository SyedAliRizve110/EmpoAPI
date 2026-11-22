using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Leaves.LeaveType.Query;

namespace Empo.EmployeeService.Application.Leaves.LeaveType.Handler;

public class GetLeaveTypeListQueryHandler : IQueryHandler<GetLeaveTypeListQuery, GetLeaveTypeListResponse>
{
    public ILeaveTypeService _service { get; }

    public GetLeaveTypeListQueryHandler(ILeaveTypeService service)
    {
        _service = service;
    }

    public async Task<GetLeaveTypeListResponse> Handle(GetLeaveTypeListQuery query, CancellationToken cancellationToken)
    {
        var response = await _service.ListAsync(query.request);
        return response;
    }
}
