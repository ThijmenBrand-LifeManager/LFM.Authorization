using LFM.Authorization.AspNetCore.Models;
using LFM.Authorization.Core.Models;
using LFM.Authorization.Repository.Interfaces;
using MediatR;

namespace LFM.Authorization.Application.Queries;

public record GetRoleAssignmentsQuery(string UserId) : IRequest<IEnumerable<RoleAssignment>>;

public class GetRoleAssignmentsQueryHandler(IRoleAssignmentRepository roleAssignmentRepository) : IRequestHandler<GetRoleAssignmentsQuery, IEnumerable<RoleAssignment>>
{
    public async Task<IEnumerable<RoleAssignment>> Handle(GetRoleAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var roleAssignments = await roleAssignmentRepository.GetAllForUserByScope(request.UserId, cancellationToken);
        return roleAssignments;
    }
}