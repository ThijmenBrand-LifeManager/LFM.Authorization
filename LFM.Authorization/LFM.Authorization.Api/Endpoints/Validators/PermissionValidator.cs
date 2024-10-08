using FluentValidation;
using LFM.Authorization.Endpoints.Dto;

namespace LFM.Authorization.Endpoints.Validators;

public class PermissionValidator : AbstractValidator<CreatePermissionDto>
{
    public PermissionValidator()
    {
        RuleFor(permission => permission.Name).NotNull().NotEmpty().Length(3, 100);
        RuleFor(permission => permission.Description).MaximumLength(500);
    }
}