using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LFM.Authorization.AspNetCore.Models;

[PrimaryKey(nameof(UserId), nameof(Scope))]
public class RoleAssignment
{
    public string UserId { get; init; }
    public string Scope { get; init; }
    
    public virtual LfmRole Role { get; init; } = null!;
}