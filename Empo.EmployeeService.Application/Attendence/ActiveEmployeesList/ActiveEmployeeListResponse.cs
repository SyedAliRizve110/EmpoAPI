using Empo.EmployeeService.Application.Employees.EmployeesModel;
using Empo.EmployeeService.Application.Enums;

namespace Empo.EmployeeService.Application.Attendence.ActiveEmployeesList;

public class ActiveEmployeeListResponse
{
    public IEnumerable<ActiveListUserModel> Collection { get; set; }
    public long TotalRecords { get; set; }
}

public class ActiveListUserModel
{
    public Guid EmployeeId { get; set; }
    public string Name { get; set; }
    public AttendanceSessionStatus Status { get; set; }

}
