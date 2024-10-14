using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LFM.Authorization.AspNetCore.Models;

[PrimaryKey(nameof(UserId), nameof(Scope))]
public class RoleAssignment
{
    [Column(Order = 1)] public required string UserId { get; set; }
    [Column(Order = 2)] public required string Scope { get; set; }
    public required string RoleId { get; set; }
    public virtual LfmRole Role { get; init; } = null!;
}