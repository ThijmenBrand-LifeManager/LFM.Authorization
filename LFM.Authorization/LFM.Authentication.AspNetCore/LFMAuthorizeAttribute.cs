using System.Security.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LFM.Authentication.AspNetCore;

public class LfmAuthorizeAttribute() : TypeFilterAttribute(typeof(LfmAuthorizationFilter));

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class LfmAuthorizationFilter : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        var isAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
        if (!isAuthenticated) throw new AuthenticationException("User is not authenticated");

        return;
    }
}