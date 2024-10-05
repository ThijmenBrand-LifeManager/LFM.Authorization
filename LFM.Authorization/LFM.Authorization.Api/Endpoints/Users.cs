using System.Security.Claims;
using LFM.Authorization.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace LFM.Authorization.Endpoints;

public class Users(IHttpContextAccessor httpContextAccessor) : ControllerBase
{
    [HttpGet("me")]
    [LfmAuthorize]
    public async Task<IActionResult> Me()
    {
        var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

        return Ok(userId);
    }
}