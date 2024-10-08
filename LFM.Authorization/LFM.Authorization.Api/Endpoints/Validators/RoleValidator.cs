using FluentValidation;
using LFM.Authorization.Core.Models;
using LFM.Authorization.Endpoints.Dto;

namespace LFM.Authorization.Endpoints.Validators;

public class RoleValidator : AbstractValidator<CreateRoleDto>
{
    public RoleValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(3, 50);
        RuleFor(x => x.ScopeMask).NotEmpty();
    }
}