using LFM.Authorization.Application.AuthHelpers;
using LFM.Authorization.Core.Models;
using LFM.Authorization.Endpoints.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace LFM.Authorization.Endpoints;

[ApiController]
[Route("[controller]")]
public class AccountController(
    UserManager<LfmUser> userManager,
    ITokenService tokenService,
    SignInManager<LfmUser> signInManager)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user == null) return Unauthorized();
        
        var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!result.Succeeded) return Unauthorized();
        
        var token = tokenService.CreateToken(user);
        
        return Ok(token);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var user = new LfmUser
        {
            Email = model.Email,
            UserName = model.Username,
        };
        
        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);
        
        return Ok();
    }
    
}