namespace LFM.Authorization.Endpoints.Dto;

public class CreatePermissionDto
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string? Description { get; set; }
}