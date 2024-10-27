using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LFM.Authorization.AspNetCore.Models;

public class RoleAssignment
{
    [Column(Order = 0)] public string UserId { get; init; }
    [Column(Order = 1)] public string Scope { get; init; }
    public string RoleName { get; set; }
    public string RoleScope { get; set; }
    
    public virtual LfmRole Role { get; init; } = null!;
}