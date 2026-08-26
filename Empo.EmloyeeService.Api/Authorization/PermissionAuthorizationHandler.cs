using Microsoft.AspNetCore.Authorization;

namespace Empo.EmloyeeService.Api.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (!context.User.Identity?.IsAuthenticated ?? true)
                return Task.CompletedTask;
            bool hasPermission = context.User.Claims
                .Any(c => c.Type == "permission" && c.Value == requirement.Permission);

            if (hasPermission)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
