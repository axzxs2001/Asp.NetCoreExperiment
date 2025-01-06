using Microsoft.AspNetCore.Authorization;

namespace OpenAPIDemoForAI
{
    public class PermissionHandler : AuthorizationHandler<AuthorizationRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirement requirement)
        {
            if (context.Resource is DefaultHttpContext)
            {
                var httpContext = context.Resource as DefaultHttpContext;
                var questPath = httpContext?.Request?.Path;
                var method = httpContext?.Request?.Method;
                var isAuthenticated = context?.User?.Identity?.IsAuthenticated;
                if (isAuthenticated.HasValue && isAuthenticated.Value)
                {
                    context?.Succeed(requirement);
                }
                else
                {
                    context?.Fail();
                }
            }
            await Task.CompletedTask;
        }
    }
    public class AuthorizationRequirement : IAuthorizationRequirement
    {
    }
}
