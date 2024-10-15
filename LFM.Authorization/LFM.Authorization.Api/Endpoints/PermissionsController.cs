using FluentValidation;
using LFM.Authorization.Application.Commands;
using LFM.Authorization.Application.Queries;
using LFM.Authorization.AspNetCore;
using LFM.Authorization.Endpoints.Dto;
using LFM.Authorization.Endpoints.Validators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LFM.Authorization.Endpoints;

[ApiController]
[Route("[controller]")]
public class PermissionsController(ISender sender, PermissionValidator validator) : ControllerBase
{
    [HttpGet(Name = "ListPermissions")]
    public async Task<IResult> ListPermissions()
    {
        var permissions = await sender.Send(new ListPermissionsQuery());
        return Results.Ok(permissions);
    }
    
    [HttpPost(Name = "CreatePermission")]
    public async Task<IResult> CreatePermission([FromBody] CreatePermissionDto command)
    {
        ValidateDto(command);
        var permission = await sender.Send(new CreatePermissionCommand(command.Name, command.Category, command.Description));
        return Results.Ok(permission);
    }
    
    private void ValidateDto(CreatePermissionDto dto)
    {
        var result = validator.Validate(dto);
        if (!result.IsValid)
        {   
            throw new ValidationException(result.Errors);
        }
    }
}