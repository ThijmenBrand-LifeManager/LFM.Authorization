using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LFM.Authorization.AspNetCore.Models;

public class Permission
{
    [Key] public required string Name { get; set; }
    public string? Description { get; set; }

    [JsonIgnore] public ICollection<LfmRole>? Roles { get; set; } = [];
}