using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LFM.Authorization.AspNetCore.Models;

[PrimaryKey(nameof(UserId), nameof(Scope))]
public class RoleAssignment
{
    [Column(Order = 0)] public required string UserId { get; init; }
    [Column(Order = 1)] public required string Scope { get; init; }
    public required string RoleName { get; init; }
    public required string RoleScope { get; init; }
    
    [ForeignKey(nameof(RoleName) + "," + nameof(RoleScope))]
    public virtual LfmRole Role { get; init; } = null!;
}