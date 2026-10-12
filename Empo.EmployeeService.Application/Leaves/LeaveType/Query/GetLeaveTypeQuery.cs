using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.Leaves.LeaveType.Query;

public class GetLeaveTypeQuery : IQuery<LeaveTypeRequestModel>
{
    public Guid _leaveTypeId { get; private set; }
    public GetLeaveTypeQuery()
    {

    }

    public static GetLeaveTypeQuery Create(Guid leaveTypeId)
    {
        return new GetLeaveTypeQuery()
        {
            _leaveTypeId = leaveTypeId
        };
    }
}
