using AutoMapper;
using Empo.EmployeeService.Application.AuthService;
using Empo.EmployeeService.Application.Interface;
using Empo.EmployeeService.Infrastructure.Data.Entities.User;
using Microsoft.AspNetCore.Identity;

namespace Empo.EmployeeService.Infrastructure.Data.Services;

public class PasswordHasherService : IPasswordHasherService
{
    private readonly PasswordHasher<UserEntity> _hasher;
    private readonly IMapper _mapper;

    public PasswordHasherService(IMapper mapper)
    {
        _mapper = mapper;
        _hasher = new PasswordHasher<UserEntity>();
    }

    public async Task<string> HashPassword(UserModel user, string password)
    {
        var userEntity = _mapper.Map<UserEntity>(user);
        return _hasher.HashPassword(userEntity, password);
    }

    public async Task<bool> VerifyPassword(UserModel _user, string password, string passwordHash)
    {
        var user = _mapper.Map<UserEntity>(_user);
        var result = _hasher.VerifyHashedPassword(user, passwordHash, password);
        return result == PasswordVerificationResult.Success ||
          result == PasswordVerificationResult.SuccessRehashNeeded;
    }
}
