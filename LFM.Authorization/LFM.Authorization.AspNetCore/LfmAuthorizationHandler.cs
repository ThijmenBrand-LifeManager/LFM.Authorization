using Microsoft.AspNetCore.Authorization;

namespace LFM.Authorization.AspNetCore;

public class LfmAuthorizationHandler : AuthorizationHandler<LfmAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LfmAuthorizationRequirement requirement)
    {
        var user = context.User.Identity.IsAuthenticated;
        if (!user)
        {
            context.Fail();
            return Task.CompletedTask;
        }
        
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}