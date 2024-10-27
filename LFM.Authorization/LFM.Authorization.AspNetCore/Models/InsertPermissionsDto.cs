namespace LFM.Authorization.AspNetCore.Models;

public class PermissionDto
{
    public required string Name { get; init; }
    public required string Category { get; init; }
}
public class InsertMultiplePermissionsOnMultipleRolesDto
{
    public IEnumerable<PermissionDto> Permissions { get; init; }
    public IEnumerable<DefaultRoles> Roles { get; init; }
}

public class InsertMultiplePermissionsOnSingleRoleDto
{
    public IEnumerable<PermissionDto> Permissions { get; init; }
    public DefaultRoles Role { get; init; }
}

public class InsertPermissionOnMultipleRolesDto
{
    public PermissionDto Permission { get; init; }
    public IEnumerable<DefaultRoles> Roles { get; init; }
}

public class InsertPermissionOnSingleRoleDto
{
    public PermissionDto Permission { get; init; }
    public DefaultRoles Role { get; init; }
}