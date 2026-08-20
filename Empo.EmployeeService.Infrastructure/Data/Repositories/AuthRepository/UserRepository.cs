using AutoMapper;
using Empo.EmployeeService.Application.AuthService;
using Empo.EmployeeService.Infrastructure.Data.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories.AuthRepository;

public class UserRepository : IUserService
{
    private readonly EmployeeContext _dbContext;
    private IMapper _mapper { get; }

    public UserRepository(EmployeeContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }
    public async Task AddRefreshTokwnAsync(RefreshTokenModel _refreshToken)
    {
        var refreshToken = _mapper.Map<RefreshTokenEntity>(_refreshToken);
        await _dbContext.RefreshToken.AddAsync(refreshToken);
    }

    public async Task<UserModel?> GetByEmailAsync(string email)
    {
        var user = await _dbContext.User
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.Email == email);
        var userModel = _mapper.Map<UserModel>(user);
        return userModel;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
