using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.Authorization.AspNetCore;

public static class AspNetCoreModule
{
    public static IServiceCollection AddLfmAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthorization();
        services.AddSingleton<IAuthorizationHandler, LfmAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, LfmAuthorizationPolicyProvider>();

        return services;
    }

}