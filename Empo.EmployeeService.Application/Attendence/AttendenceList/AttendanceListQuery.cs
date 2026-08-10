using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.Attendence.AttendenceList;

public class AttendanceListQuery : IQuery<AttendanceListResponse>
{
    public AttendanceListRequest request { get; private set; }

    public static AttendanceListQuery Create(AttendanceListRequest _request)
    {
        return new AttendanceListQuery()
        {
            request = _request
        };
    }
}
