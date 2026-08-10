using Empo.EmployeeService.Application.Attendence.ActiveEmployeesList;
using Empo.EmployeeService.Application.Attendence.AttendenceList;

namespace Empo.EmployeeService.Application.Attendence.ServiceInterface;

public interface IAttendanceService
{
    Task<bool> CheckAnyOpenSession(Guid employeeId);
    Task<EmployeeAttendanceModel> GetOpenSession(Guid employeeId);
    Task<Guid> ClockIn(EmployeeAttendanceModel model);
    Task<Guid> ClockOut(EmployeeAttendanceModel model);
    Task<AttendanceListResponse> ListAsync(AttendanceListRequest request);
    Task<ActiveEmployeeListResponse> ActiveEmployeeListAsync(ActiveEmployeeListRequest request);
}
