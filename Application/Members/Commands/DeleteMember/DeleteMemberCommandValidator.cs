using FluentValidation;

namespace Application.Members.Commands.DeleteMember;

internal sealed class DeleteMemberCommandValidator : AbstractValidator<DeleteMemberCommand>
{
    public DeleteMemberCommandValidator()
    {
        RuleFor(m => m.MemberId).NotEmpty().WithMessage("User Id is required.");
    }
}
