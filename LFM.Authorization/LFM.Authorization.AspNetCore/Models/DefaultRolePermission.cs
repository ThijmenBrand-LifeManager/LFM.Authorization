using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LFM.Authorization.AspNetCore.Models;

[PrimaryKey(nameof(Role), nameof(PermissionName))]
public class DefaultRolePermission
{
    public string Role { get; init; }
    public string PermissionName { get; init; }
    public Permission Permission { get; init; } = null;
}