using Microsoft.AspNetCore.Identity.Data;

namespace LFM.Authorization.Endpoints.Dto;

public class RegisterDto
{
    /// <summary>
    /// The user's email address, which is the user's unique identifier.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// The user's password.
    /// </summary>
    public required string Password { get; init; }
    
    /// <summary>
    /// The user's username.
    /// </summary>
    public string Username { get; init; }
}