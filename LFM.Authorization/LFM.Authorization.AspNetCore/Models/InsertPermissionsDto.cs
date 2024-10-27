namespace LFM.Authorization.AspNetCore.Models;

public class InsertPermissionsDto
{
    public required string Name { get; init; }
    public required string Category { get; init; }
}