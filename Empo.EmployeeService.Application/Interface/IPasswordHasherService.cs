using Empo.EmployeeService.Application.AuthService;

namespace Empo.EmployeeService.Application.Interface;

public interface IPasswordHasherService
{
    Task<string> HashPassword(UserModel user, string password);
    Task<bool> VerifyPassword(UserModel _user, string hashedPassword, string providedPassword);
}
