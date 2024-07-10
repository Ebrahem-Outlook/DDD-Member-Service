using FluentValidation;

namespace Application.Members.Commands.CreateMember;

internal sealed class CreateMemberValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberValidator()
    {
        RuleFor(m => m.FirstName).NotEmpty().WithMessage("First Name of user is required.");

        RuleFor(m => m.LastName).NotEmpty().WithMessage("First Name of user is required.");

        RuleFor(m => m.Email).NotEmpty().WithMessage("First Name of user is required.");

        RuleFor(m => m.Password).NotEmpty().WithMessage("First Name of user is required.");
    }
}
