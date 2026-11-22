using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Empo.BuildingBlocks.Infrastructure.Configuration.Auth;

/// <summary>
/// Shared JWT validation & permission authorization extensions used by any service
/// that accepts tokens issued by the EmployeeService / AuthService.
/// </summary>
public static class JwtExtensions
{
    public static IServiceCollection AddSharedJwtAuth(
        this IServiceCollection services,
        IConfiguration config)
    {
        var rawKey = config["Jwt:Key"]
            ?? throw new InvalidOperationException(
                "Jwt:Key is missing from configuration. " +
                "Add it to appsettings.Development.json or an environment variable.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(rawKey));

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer           = true,
                    ValidateAudience         = true,
                    ValidateLifetime         = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer              = config["Jwt:Issuer"],
                    ValidAudience            = config["Jwt:Audience"],
                    IssuerSigningKey         = key,
                    ClockSkew                = TimeSpan.FromSeconds(30)
                };

                // Keep claim type names as they come from the token (sub, email, etc.)
                // instead of mapping them to long Microsoft URI-style names.
                options.MapInboundClaims = false;
            });

        return services;
    }

    /// <summary>
    /// Registers the dynamic permission authorization provider and handler.
    /// Reads "permission" claims directly from the validated JWT token without DB access.
    /// </summary>
    public static IServiceCollection AddPermissionAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        return services;
    }
}
