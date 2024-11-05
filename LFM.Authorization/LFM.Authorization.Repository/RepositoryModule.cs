using LFM.Authorization.Repository.Interfaces;
using LFM.Authorization.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.Authorization.Repository;

public static class RepositoryModule
{
    public static IServiceCollection AddRepositoryModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql(configuration.GetSection("Postgres").GetValue<string>("ConnectionString")));

        services.AddTransient<IPermissionRepository, PermissionRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IRoleAssignmentRepository, RoleAssignmentRepository>();
        services.AddTransient<IDefaultRolePermissionRepository, DefaultRolePermissionRepository>();
        
        return services;
    }
}