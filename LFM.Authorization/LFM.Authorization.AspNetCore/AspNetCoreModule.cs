using System.Text;
using LFM.Authorization.AspNetCore.Database;
using LFM.Authorization.AspNetCore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

namespace LFM.Authorization.AspNetCore;

public static class AspNetCoreModule
{
    public static PermissionsBuilder AddLfmAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseConfiguration = configuration.GetSection("AuthorizationDatabase");
        var sslmode = configuration.GetValue<string>("Environment") == "Production" ? SslMode.Require : SslMode.Prefer;
        var connectionString = new NpgsqlConnectionStringBuilder
        {
            Host = databaseConfiguration.GetValue<string>("Host"),
            Port = databaseConfiguration.GetValue<int>("Port"),
            Database = databaseConfiguration.GetValue<string>("Database"),
            Username = databaseConfiguration.GetValue<string>("Username"),
            SslMode = sslmode,
            Password = configuration.GetSection("Database").GetValue<string>("Password")
        }.ToString();
        
        services.AddDbContext<AuthorizationDbContext>(options =>
            options.UseNpgsql(connectionString));
        
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

        return new PermissionsBuilder(context);
    }

}