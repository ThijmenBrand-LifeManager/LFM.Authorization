using LFM.Authorization.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace LFM.Authorization.Endpoints;

public class Users(IHttpContextAccessor httpContextAccessor) : ControllerBase
{
    [HttpGet("me")]
    [LfmAuthorize(["user.read"], ["/profile"])]
    public async Task<IActionResult> Me()
    {
        var userId = httpContextAccessor.HttpContext.User.Identity.Name;

        return Ok(userId);
    }
}