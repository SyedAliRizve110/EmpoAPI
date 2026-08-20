namespace Empo.EmployeeService.Application.AuthService;

public interface IUserService
{
    Task<UserModel?> GetByEmailAsync(string email);
    Task AddRefreshTokwnAsync(RefreshTokenModel refreshTokn);
    Task SaveChangesAsync();
}
