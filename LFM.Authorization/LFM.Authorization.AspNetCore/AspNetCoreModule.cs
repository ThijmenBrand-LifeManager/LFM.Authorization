using System.Text;
using LFM.Authorization.AspNetCore.Database;
using LFM.Authorization.AspNetCore.Services;
using LFM.Authorization.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LFM.Authorization.AspNetCore;

public static class AspNetCoreModule
{
    public static PermissionsBuilder AddLfmAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWT>(configuration.GetSection("Jwt"));
        services.AddDbContext<AuthorizationDbContext>(options =>
            options.UseNpgsql(configuration.GetSection("Authorization").GetValue<string>("ConnectionString")));
        
        services.AddTransient<IValidatePermissions, ValidatePermissions>();
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(config =>
        {
            config.RequireHttpsMetadata = false;
            config.SaveToken = false;
            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
            };
        });
        services.AddAuthorization();

        services.AddSingleton<IAuthorizationHandler, LfmAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, LfmAuthorizationPolicyProvider>();
        
        var scope = services.BuildServiceProvider().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AuthorizationDbContext>();

        return new PermissionsBuilder(services, context);
    }

}