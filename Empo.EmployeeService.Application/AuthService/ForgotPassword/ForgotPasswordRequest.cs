using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.AuthService.ForgotPassword;

public class ForgotPasswordRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
