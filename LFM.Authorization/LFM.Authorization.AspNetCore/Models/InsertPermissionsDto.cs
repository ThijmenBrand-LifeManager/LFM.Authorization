namespace LFM.Authorization.AspNetCore.Models;

public class PermissionDto
{
    public required string Name { get; init; }
    public required string Category { get; init; }
}
public class InsertMultiplePermissionsOnMultipleRolesDto
{
    public IEnumerable<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
    public IEnumerable<DefaultRoles> Roles { get; set; } = new List<DefaultRoles>();
}

public class InsertMultiplePermissionsOnSingleRoleDto
{
    public IEnumerable<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
    public DefaultRoles Role { get; set; }
}

public class InsertPermissionOnMultipleRolesDto
{
    public PermissionDto Permission { get; set; }
    public IEnumerable<DefaultRoles> Roles { get; set; }
}

public class InsertPermissionOnSingleRoleDto
{
    public PermissionDto Permission { get; init; }
    public DefaultRoles Role { get; init; }
}