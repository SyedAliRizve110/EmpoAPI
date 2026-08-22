using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.AuthService.ForgotPassword;

public class ForgotPasswordCommand : CommandBase<bool>
{
    public ForgotPasswordRequest _request { get; set; }

    public static ForgotPasswordCommand Create(ForgotPasswordRequest request)
    {
        return new ForgotPasswordCommand() { _request = request };
    }
}
