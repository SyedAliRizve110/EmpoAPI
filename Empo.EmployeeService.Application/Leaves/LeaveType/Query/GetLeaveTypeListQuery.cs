using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.Leaves.LeaveType.Query;

public class GetLeaveTypeListQuery : IQuery<GetLeaveTypeListResponse>
{
    public GetLeaveTypeListRequest request { get; set; }
    public GetLeaveTypeListQuery()
    { }

    public static GetLeaveTypeListQuery Create(GetLeaveTypeListRequest request)
    {
        return new GetLeaveTypeListQuery()
        {
            request = request
        };
    }
}
