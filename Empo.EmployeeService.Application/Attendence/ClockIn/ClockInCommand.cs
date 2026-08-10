using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Attendence.ClockIn;

public class ClockInCommand : CommandBase<EmployeeDto>
{
    public ClockInRequest _request { get; set; }

    public static ClockInCommand Create(ClockInRequest request)
    {
        return new ClockInCommand
        {
            _request = request
        };
    }
}
