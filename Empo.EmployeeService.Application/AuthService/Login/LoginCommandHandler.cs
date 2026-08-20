using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Interface;

namespace Empo.EmployeeService.Application.AuthService.Login;

public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly IUserService _service;
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasherService _passwordHasher;

    public LoginCommandHandler(IUserService service, IJwtService jwtservice, IPasswordHasherService passwordHasher)
    {
        _service = service;
        _jwtService = jwtservice;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var _request = request.request;
        var user = await _service.GetByEmailAsync(_request.Email);

        if (user == null)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password."
                );
        }

        if (!user.IsActive)
        {
            throw new UnauthorizedAccessException(
                "User account is inactive"
        );
        }

        // Verify Password
        var passwordResult = _passwordHasher.VerifyPassword(user,
            _request.Password, user.PasswordHash
            );
        if (passwordResult.Result == false)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password."
                );
        }

        //Genrate access token

        var accessToken = _jwtService.GenerateAccessToken(user);

        var accessTokenExpiry = _jwtService.GetAccessTokenExpiry();

        // Generate refresh token 

        var refreshToken = _jwtService.GenerateRefreshToken();

        // Save refresh token

        var refreshRokenModel = new RefreshTokenModel
        {
            UserId = user.Id,
            Token = refreshToken,
            ExpiresAt = accessTokenExpiry.AddMinutes(30)
        };
        await _service.AddRefreshTokwnAsync(refreshRokenModel);

        // Update last login
        user.LastLoginAt = DateTime.UtcNow;
        await _service.SaveChangesAsync();

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiresAt = accessTokenExpiry,
            UserId = user.Id,
            EmployeeId = user.EmployeeId
        };
    }
}