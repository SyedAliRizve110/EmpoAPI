namespace Empo.EmployeeService.Application.Attendence.AttendenceList;

public class AttendanceListResponse
{
    public IEnumerable<EmployeeAttendanceModel> Collection { get; set; }
    public long TotalRecords { get; set; }
}