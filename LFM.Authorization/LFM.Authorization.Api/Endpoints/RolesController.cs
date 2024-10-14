using FluentValidation;
using LFM.Authorization.Application.Commands;
using LFM.Authorization.Application.Queries;
using LFM.Authorization.AspNetCore;
using LFM.Authorization.AspNetCore.Services;
using LFM.Authorization.Endpoints.Dto;
using LFM.Authorization.Endpoints.Validators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LFM.Authorization.Endpoints;

[ApiController]
[Route("[controller]")]
public class RolesController(ISender sender, RoleValidator validator, IHttpContextAccessor httpContextAccessor) : ControllerBase
{
    
    [HttpPost(Name = "CreateRole")]
    public async Task<IResult> CreateRole([FromBody] CreateRoleDto command)
    {
        ValidateDto(command);
        var role = await sender.Send(new CreateRoleCommand(command.Name, command.ScopeMask, command.Description));
        return Results.Ok(role);
    }
    
    [HttpGet("{workstreamId}/listByScope", Name = "ListRolesByScope")]
    public async Task<IResult> ListRoles([FromRoute] string workstreamId)
    {
        var scope = $"/workstream/{workstreamId}";
        var roles = await sender.Send(new ListRolesQuery(scope));
        return Results.Ok(roles);
    }
    
    [HttpGet("list/{workstreamId}", Name = "ListAllRole")]
    [LfmAuthorize(["workstream.role.read"], [ScopeHelper.ScopeMaskWorkStream])]
    public async Task<IResult> ListAllRoles(string workstreamId)
    {
        var context = httpContextAccessor.HttpContext?.GetRouteData().Values;
        var roles = await sender.Send(new ListAllRolesQuery());
        return Results.Ok(roles);
    }

    [HttpPut("{roleId}/addPermission")]
    public async Task<IResult> AddPermissionToRole([FromRoute] string roleId, [FromBody] string permission)
    {
        var result = await sender.Send(new AddPermissionToRoleCommand(roleId, permission));
        return Results.Ok(result);
    }
    
    private void ValidateDto(CreateRoleDto dto)
    {
        var result = validator.Validate(dto);
        if (!result.IsValid)
        {   
            throw new ValidationException(result.Errors);
        }
    }
}