using Empo.EmployeeService.Application.AuthService;

namespace Empo.EmployeeService.Application.Interface;

public interface IJwtService
{
    string GenerateAccessToken(UserModel user, IEnumerable<string> permission);
    string GenerateRefreshToken();
    DateTime GetAccessTokenExpiry();
}
