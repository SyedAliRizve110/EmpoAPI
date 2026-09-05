using AutoMapper;
using Empo.EmployeeService.Application.AuthService;
using Empo.EmployeeService.Application.Interface;
using Empo.EmployeeService.Infrastructure.Data.Entities.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Empo.EmployeeService.Api.Jwt;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    public JwtService(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }

    public string GenerateAccessToken(UserModel _user, IEnumerable<string> permissions)
    {
        var user = _mapper.Map<UserEntity>(_user);
        var claims = new List<Claim>()
        {
            new Claim(
                JwtRegisteredClaimNames.Sub,
                user.Id.ToString()
                ),
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()
                ),
            new Claim(
                ClaimTypes.Email,
                user.Email
                ),
            new Claim(
                "EmployeeId",
                user.EmployeeId.ToString()
                ),
            new Claim(
                ClaimTypes.Role,
                user.Role.Name
                ),
            new Claim(
                ClaimTypes.Name,
                user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti,
            Guid.NewGuid().ToString())
        };

        foreach (var permision in permissions)
        {
            claims.Add(new Claim("permission", permision));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!
                )
            );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
            );

        var expires = GetAccessTokenExpiry();

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: credentials
            );
        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(randomBytes);
    }

    public DateTime GetAccessTokenExpiry()
    {
        return DateTime.UtcNow.AddHours(10);
    }

}
