using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LFM.Authorization.AspNetCore.Models;

public class LfmRole
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public string Id { get; set; }
    public required string Name { get; set; }
    public required string Scope { get; set; }
    public string? Description { get; set; }
    public ICollection<Permission> Permissions { get; } = [];
    [JsonIgnore] public ICollection<RoleAssignment> RoleAssignments { get; } = [];
}