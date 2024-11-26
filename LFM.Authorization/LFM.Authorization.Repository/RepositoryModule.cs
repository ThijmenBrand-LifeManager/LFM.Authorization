using Azure.Core;
using Azure.Identity;
using LFM.Authorization.Repository.Interfaces;
using LFM.Authorization.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client.AppConfig;
using Npgsql;

namespace LFM.Authorization.Repository;

public static class RepositoryModule
{
    public static IServiceCollection AddRepositoryModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = new NpgsqlConnectionStringBuilder
        {
            Host = "lfm-pgsql-db-dev.postgres.database.azure.com",
            Port = 5432,
            Database = "lfm-authorization",
            Username = "lfm_authorization_service",
            SslMode = SslMode.Require,
            Password = configuration["Database:Password"]
        }.ToString();
        
        services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddTransient<IPermissionRepository, PermissionRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IRoleAssignmentRepository, RoleAssignmentRepository>();
        services.AddTransient<IDefaultRolePermissionRepository, DefaultRolePermissionRepository>();
        
        return services;
    }
}