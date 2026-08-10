using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Attendence.ClockIn;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Attendence.ClockOut;

public class ClockOutCommand : CommandBase<EmployeeDto>
{
    public ClockOutRequest _request { get; set; }

    public static ClockOutCommand Create(ClockOutRequest request)
    {
        return new ClockOutCommand
        {
            _request = request
        };
    }
}
