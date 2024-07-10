using FluentValidation;

namespace Application.Members.Commands.UpdateEmail;

internal sealed class UpdateEmailCommandValidator : AbstractValidator<UpdateEmailCommand>
{
    public UpdateEmailCommandValidator()
    {
        RuleFor(m => m.MemberId).NotEmpty().WithMessage("Id of member is required.");

        RuleFor(m => m.Email).NotEmpty().WithMessage("Email of member is required.");
    }
}
