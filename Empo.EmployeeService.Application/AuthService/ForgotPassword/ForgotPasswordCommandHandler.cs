using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.AuthService.Intrfaces;
using Empo.EmployeeService.Application.Interface;

namespace Empo.EmployeeService.Application.AuthService.ForgotPassword;

public class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand, bool>
{
    private readonly IUserService _service;
    private readonly IPasswordHasherService _passwordHasher;

    public ForgotPasswordCommandHandler(
        IUserService service,
        IPasswordHasherService passwordHasher
        )
    {
        _service = service;
        _passwordHasher = passwordHasher;
    }

    public async Task<bool> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var user = await _service.GetByEmailAsync(request.Email);
        if (user == null)
            throw new Exception("No user found with this email");
        else
        {
            var hashedPassword = await _passwordHasher.HashPassword(request.Email, request.Password);
            var isPassUpdated = await _service.UpdatePasswordAsync(request.Email, hashedPassword);
            return isPassUpdated;
        }
    }
}
