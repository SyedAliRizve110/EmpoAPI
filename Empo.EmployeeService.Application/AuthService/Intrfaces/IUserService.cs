namespace Empo.EmployeeService.Application.AuthService.Intrfaces;

public interface IUserService
{
    Task<UserModel?> GetByEmailAsync(string email);
    Task AddRefreshTokwnAsync(RefreshTokenModel refreshTokn);
    Task SaveChangesAsync();
    Task<bool> UpdatePasswordAsync(string email, string HashedPassword);
}
