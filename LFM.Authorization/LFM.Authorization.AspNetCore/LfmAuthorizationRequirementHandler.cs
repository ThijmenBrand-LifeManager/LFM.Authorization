using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace LFM.Authorization.AspNetCore;

public class LfmAuthorizationRequirementHandler(IHttpContextAccessor httpContextAccessor)
    : AuthorizationHandler<LfmAuthorizeRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        LfmAuthorizeRequirement requirement)
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