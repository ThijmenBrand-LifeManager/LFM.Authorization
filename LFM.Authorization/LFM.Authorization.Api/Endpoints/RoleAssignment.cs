using LFM.Authorization.Application.Commands;
using LFM.Authorization.Application.Queries;
using LFM.Authorization.Endpoints.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LFM.Authorization.Endpoints;

[ApiController]
[Route("[controller]")]
public class RoleAssignment(ISender sender) : ControllerBase
{
    [HttpPost(Name = "CreateRoleAssignment")]
    public async Task<IResult> CreateRoleAssignment(CreateRoleAssignmentDto roleAssignment)
    {
        var result = await sender.Send(new CreateRoleAssignmentCommand(roleAssignment.UserId, roleAssignment.Role, roleAssignment.Scope));
        return Results.Ok(result);
    }
    
    [HttpPost("user/{userId}/", Name = "GetRoleAssignmentsByUserByScope")]
    public async Task<IResult> GetRoleAssignmentsByUserByScope([FromRoute] string userId)
    {
        var result = await sender.Send(new GetRoleAssignmentsQuery(userId));
        return Results.Ok(result);
    }
}