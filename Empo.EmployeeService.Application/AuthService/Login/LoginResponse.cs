namespace Empo.EmployeeService.Application.AuthService.Login;

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiresAt { get; set; }
    public Guid UserId { get; set; }
    public Guid EmployeeId { get; set; }
    public string Role { get; set; }
}
