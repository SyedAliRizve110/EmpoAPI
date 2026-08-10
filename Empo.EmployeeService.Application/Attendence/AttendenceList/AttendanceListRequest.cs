using Empo.BuildingBlocks.Application.SharedModels;

namespace Empo.EmployeeService.Application.Attendence.AttendenceList;

public class AttendanceListRequest //: ModelFilterBase
{
    public Guid? employeeId { get; set; } = null;
    public DateOnly? Date { get; set; } = null;
}
