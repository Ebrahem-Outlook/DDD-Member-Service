using FluentValidation;

namespace Application.Members.Commands.UpdatePassword;

internal sealed class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordCommandValidator()
    {
        RuleFor(m => m.MemberId).NotEmpty().WithMessage("Member Id is required.");

        RuleFor(m => m.Password).NotEmpty().WithMessage("Member Password is required.");
    }
}
