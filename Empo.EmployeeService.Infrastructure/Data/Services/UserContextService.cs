using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.BuildingBlocks.Infrastructure.Configuration.UserConfiguration;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Empo.EmployeeService.Infrastructure.Data.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public Guid? GetUserId()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext?.User?.Identity?.IsAuthenticated != true)
            return null;

        var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userId, out var id))
            return null;
        return id;
    }
    public string GetEmail()
    {
        var email = _httpContextAccessor.HttpContext?
    .User
    .FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
            return null;
        return email;
    }

    public Guid? GetEmployeeId()
    {
        var employeeId = _httpContextAccessor.HttpContext?
    .User
    .FindFirst("EmployeeId").Value;
        if (!Guid.TryParse(employeeId, out var id))
            return null;

        return id;
    }

    public string? GetRole()
    {
        return _httpContextAccessor.HttpContext?.User?
            .FindFirst(ClaimTypes.Role)?.Value;
    }
}
