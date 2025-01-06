using LFM.Authorization.Application.AuthHelpers;
using LFM.Authorization.Core.Models;
using LFM.Authorization.Endpoints.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LFM.Authorization.Endpoints;

[ApiController]
[Route("auth")]
public class AccountController(
    UserManager<LfmUser> userManager,
    ITokenService tokenService,
    SignInManager<LfmUser> signInManager,
    ILogger<AccountController> logger)
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
        var authentication = new AuthenticationDto()
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email!,
            Token = token,
        };
        
        Log.Information("User {Username} logged in from {Ip}", user.UserName, HttpContext.Connection.RemoteIpAddress);
        
        return Ok(authentication);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var newUser = new LfmUser
        {
            Email = model.Email,
            UserName = model.Username,
        };
        
        var result = await userManager.CreateAsync(newUser, model.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);

        var user = await userManager.FindByEmailAsync(newUser.Email);
        if (user == null) return Unauthorized();
        
        var token = tokenService.CreateToken(user);
        var authentication = new AuthenticationDto()
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email!,
            Token = token,
        };
        
        return Ok(authentication);
    }
    
}