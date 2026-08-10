using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Attendence.ClockIn;
using Empo.EmployeeService.Application.Attendence.ServiceInterface;
using Empo.EmployeeService.Application.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Empo.EmployeeService.Application.Attendence.ClockOut;

public class ClockOutCommandHandler : ICommandHandler<ClockOutCommand, EmployeeDto>
{
    public IAttendanceService _service { get; }
    public ClockOutCommandHandler(IAttendanceService service)
    {
        _service = service;
    }

    public async Task<EmployeeDto> Handle(ClockOutCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var sessoion = await _service.GetOpenSession(request.EmployeeId);
        if (sessoion == null)
        {
            throw new Exception("No active Session found");
        }
        else
        {
            var _updateSession = await UpdateSession(sessoion, request);
            var empId = await _service.ClockOut(_updateSession);
            return new EmployeeDto { Id = empId };

        }
    }

    public async Task<EmployeeAttendanceModel> UpdateSession(EmployeeAttendanceModel model, ClockOutRequest request)
    {
        model.ClockOutIP = request.ClockOutIP;
        model.ClockOutDevice = request.ClockOutDevice;
        model.Status = Enums.AttendanceSessionStatus.InActive;
        model.ClockOutTime = TimeOnly.FromDateTime(DateTime.UtcNow);

        var totalhours = CalculateWorkingHours(model.ClockInTime,model.ClockOutTime);

        model.WorkedHours = totalhours;
        return model;
    }
    public static double CalculateWorkingHours(TimeOnly clockIn, TimeOnly clockOut)
    {
        TimeSpan duration = clockOut - clockIn;
        if (duration < TimeSpan.Zero)
        {
            duration = duration.Add(TimeSpan.FromHours(24));
        }

        var hours = Math.Round(duration.TotalHours, 1);
        return hours;
    }
}
