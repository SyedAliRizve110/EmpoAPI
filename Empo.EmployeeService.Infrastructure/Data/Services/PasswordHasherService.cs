using AutoMapper;
using Empo.EmployeeService.Application.AuthService;
using Empo.EmployeeService.Application.Interface;
using Empo.EmployeeService.Infrastructure.Data.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data.Services;

public class PasswordHasherService : IPasswordHasherService
{
    private readonly EmployeeContext _dbContext;
    private readonly PasswordHasher<UserEntity> _hasher;
    private readonly IMapper _mapper;

    public PasswordHasherService(IMapper mapper, EmployeeContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _hasher = new PasswordHasher<UserEntity>();
    }

    public async Task<string> HashPassword(string emailId, string password)
    {
        var userEntity = await _dbContext.User
    .Include(x => x.Employee)
    .Include(x => x.Role)
    .AsNoTracking()
    .FirstOrDefaultAsync(x => x.Email == emailId);
        return _hasher.HashPassword(userEntity, password);
    }

    public async Task<bool> VerifyPassword(UserModel _user, string password, string passwordHash)
    {
        var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Id == _user.Id);
        var result = _hasher.VerifyHashedPassword(user, passwordHash, password);
        return result == PasswordVerificationResult.Success ||
          result == PasswordVerificationResult.SuccessRehashNeeded;
    }
}
