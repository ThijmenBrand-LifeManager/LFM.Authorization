using LFM.Authorization.Application.Commands;
using LFM.Authorization.Application.Queries;
using LFM.Authorization.AspNetCore;
using LFM.Authorization.AspNetCore.Services;
using LFM.Authorization.Authorization;
using LFM.Authorization.Endpoints.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LFM.Authorization.Endpoints;

[ApiController]
[Route("role-assignment")]
public class RoleAssignment(ISender sender) : ControllerBase
{
    [HttpPost]
    [LfmAuthorize([Permissions.WorkstreamConfigurer], [ScopeHelper.ScopeMaskWorkStream])]
    public async Task<IResult> CreateRoleAssignment(CreateRoleAssignmentDto roleAssignment)
    {
        var result = await sender.Send(new CreateRoleAssignmentCommand(roleAssignment.UserId, roleAssignment.Role, roleAssignment.Scope));
        return Results.Ok(result);
    }
}