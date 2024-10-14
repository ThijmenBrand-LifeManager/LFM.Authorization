using System.Security.Claims;
using LFM.Authorization.AspNetCore;
using LFM.Authorization.Core.Models;
using LFM.Authorization.Endpoints.Dto;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace LFM.Authorization.Endpoints;

public class Users(IHttpContextAccessor httpContextAccessor, UserManager<LfmUser> userManager) : ControllerBase
{
    [HttpGet("me")]
    [LfmAuthorize]
    public async Task<IActionResult> Me()
    {
        var userId = httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }
        
        var userObject = new UserDto
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email!,
        };

        return Ok(userObject);
    }

    [HttpGet("test")]
    public async Task<IActionResult> TestEndpoint()
    {
        return Ok("Hello World");
    }
}