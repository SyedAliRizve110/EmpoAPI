using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Attendence.ServiceInterface;

namespace Empo.EmployeeService.Application.Attendence.AttendenceList;

public class AttendanceListQueryHandler :IQueryHandler<AttendanceListQuery, AttendanceListResponse>

{
    public IAttendanceService _service { get; }
    public AttendanceListQueryHandler(IAttendanceService service)
    {
        _service = service;
    }

    public async Task<AttendanceListResponse> Handle(AttendanceListQuery query, CancellationToken cancellationToken)
    {
        var response = await _service.ListAsync(query.request);
        return response;
    }
}
