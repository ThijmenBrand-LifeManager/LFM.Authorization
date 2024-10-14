namespace LFM.Authorization.Endpoints.Dto;

public class CreateRoleAssignmentDto
{
    public required string UserId { get; init; }
    public required string Scope { get; init; }
    public required string Role { get; init; }
}