using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.AuthService.Login;

public class LoginCommand : CommandBase<LoginResponse>
{
    public LoginRequest request { get; set; }

    public static LoginCommand Create(LoginRequest request)
    {
        return new LoginCommand() { request = request };
    }
}
