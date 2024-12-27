using LFM.Authorization.Repository.Interfaces;
using LFM.Authorization.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace LFM.Authorization.Repository;

public static class RepositoryModule
{
    public static IServiceCollection AddRepositoryModule(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseConfiguration = configuration.GetSection("Database");
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
        
        services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddTransient<IPermissionRepository, PermissionRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IRoleAssignmentRepository, RoleAssignmentRepository>();
        services.AddTransient<IDefaultRolePermissionRepository, DefaultRolePermissionRepository>();
        
        return services;
    }
}