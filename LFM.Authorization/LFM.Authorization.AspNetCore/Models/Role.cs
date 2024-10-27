using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace LFM.Authorization.AspNetCore.Models;

public class LfmRole
{
    [Column(Order = 0)] public required string Name { get; init; }
    [Column(Order = 1)] public required string Scope { get; init; }
    public string? Description { get; init; }
    public ICollection<Permission> Permissions { get; } = [];
    [JsonIgnore] public ICollection<RoleAssignment> RoleAssignments { get; } = [];
}