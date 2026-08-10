using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Attendence.ServiceInterface;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Attendence.ClockIn;

public class ClockInCommandHandler : ICommandHandler<ClockInCommand, EmployeeDto>
{
    public IAttendanceService _service { get; }
    public ClockInCommandHandler(IAttendanceService service)
    {
        _service = service;
    }
    public async Task<EmployeeDto> Handle(ClockInCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var ifOpenSession = await _service.CheckAnyOpenSession(request.EmployeeId);
        if (!ifOpenSession)
        {
            var session = await AddClockInSession(request);
            var empId = await _service.ClockIn(session);
            return new EmployeeDto { Id = empId };
        }
        else
        {
            throw new Exception("Session is already active");
        }
    }

    public async Task<EmployeeAttendanceModel> AddClockInSession(ClockInRequest request)
    {
        var session = new EmployeeAttendanceModel()
        {
            EmployeeId = request.EmployeeId,
            ClockInDevice = request.ClockInDevice,
            ClockInIP = request.ClockInIP,
            Date = DateOnly.FromDateTime(DateTime.Today),
            ClockInTime = TimeOnly.FromDateTime(DateTime.UtcNow),
            Status = Enums.AttendanceSessionStatus.Active
        };
        return session;
    }
}
