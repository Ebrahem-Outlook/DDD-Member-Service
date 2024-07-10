using FluentValidation;

namespace Application.Members.Commands.UpdateMember;

internal sealed class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
{
    public UpdateMemberCommandValidator()
    {
        RuleFor(m => m.MemberId).NotEmpty().WithMessage("Member Id is required.");

        RuleFor(m => m.FirstName).NotEmpty().WithMessage("Member Id is required.");

        RuleFor(m => m.LastName).NotEmpty().WithMessage("Member Id is required.");
    }
}