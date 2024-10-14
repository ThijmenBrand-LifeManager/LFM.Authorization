using System.Text;
using LFM.Authorization.AspNetCore.Database;
using LFM.Authorization.AspNetCore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LFM.Authorization.AspNetCore;

public static class AspNetCoreModule
{
    public static IServiceCollection AddLfmAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthorizationDbContext>(options =>
            options.UseNpgsql(configuration.GetSection("Authorization").GetValue<string>("ConnectionString")));
        
        services.AddTransient<IValidatePermissions, ValidatePermissions>();
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(config =>
        {
            var key = configuration.GetSection("Jwt").GetValue<string>("Secret") ?? throw new ArgumentNullException();
            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration.GetSection("Jwt").GetValue<string>("Issuer"),
                ValidateAudience = true,
                ValidAudience = configuration.GetSection("Jwt").GetValue<string>("Audience"),
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidateLifetime = true,
            };
        });
        services.AddAuthorization();

        services.AddSingleton<IAuthorizationHandler, LfmAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, LfmAuthorizationPolicyProvider>();

        return services;
    }

}