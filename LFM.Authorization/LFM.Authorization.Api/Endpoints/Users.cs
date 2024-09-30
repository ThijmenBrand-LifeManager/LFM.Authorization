using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LFM.Authorization.Endpoints;

public class Users(IHttpContextAccessor httpContextAccessor) : ControllerBase
{
    [HttpGet("me")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> Me()
    {
        var userId = httpContextAccessor.HttpContext.User.Identity.Name;

        return Ok(userId);
    }
}