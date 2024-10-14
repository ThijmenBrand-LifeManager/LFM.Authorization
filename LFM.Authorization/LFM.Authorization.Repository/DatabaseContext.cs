using LFM.Authorization.AspNetCore.Models;
using LFM.Authorization.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LFM.Authorization.Repository;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : IdentityDbContext<LfmUser>(options)
{
    public DbSet<LfmRole> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RoleAssignment> RoleAssignments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("identity");
        modelBuilder.Entity<Permission>()
            .HasIndex(p => p.Name)
            .IsUnique();
        
        modelBuilder.Entity<LfmRole>()
            .HasMany(e => e.Permissions)
            .WithMany(e => e.Roles);
    }
}