using System.Data.Entity.Infrastructure;
using LFM.Authorization.AspNetCore.Models;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace LFM.Authorization.AspNetCore.Database;

public class AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : DbContext(options)
{
    public DbSet<LfmRole> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RoleAssignment> RoleAssignments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("identity");
        modelBuilder.Entity<LfmRole>()
            .HasMany(x => x.RoleAssignments)
            .WithOne(x => x.Role)
            .HasForeignKey(x => x.RoleId);
    }
}