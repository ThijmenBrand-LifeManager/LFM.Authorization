using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace LFM.Authorization.Core.Models;

[PrimaryKey(nameof(UserId), nameof(Scope))]
public class RoleAssignment
{
    [Column(Order = 1)] public required string UserId { get; set; }
    [Column(Order = 2)] public required string Scope { get; set; }
    public required string RoleId { get; set; }
    public virtual LfmRole Role { get; init; } = null!;
}