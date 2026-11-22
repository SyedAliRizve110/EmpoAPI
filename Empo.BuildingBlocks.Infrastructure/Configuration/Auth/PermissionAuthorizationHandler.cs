using Microsoft.AspNetCore.Authorization;

namespace Empo.BuildingBlocks.Infrastructure.Configuration.Auth;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (!context.User.Identity?.IsAuthenticated ?? true)
            return Task.CompletedTask;

        // Reads the "permission" claims embedded into the JWT token
        bool hasPermission = context.User.Claims
            .Any(c => c.Type == "permission" && c.Value == requirement.Permission);

        if (hasPermission)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
