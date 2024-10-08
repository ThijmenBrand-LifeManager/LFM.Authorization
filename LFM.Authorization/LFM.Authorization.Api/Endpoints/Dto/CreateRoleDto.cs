using LFM.Authorization.Core.Models;

namespace LFM.Authorization.Endpoints.Dto;

public class CreateRoleDto
{
    public string Name { get; init; }
    public string ScopeMask { get; init; }
    public string? Description { get; init; }
}