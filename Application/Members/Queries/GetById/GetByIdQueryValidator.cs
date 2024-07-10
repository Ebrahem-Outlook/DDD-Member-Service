using FluentValidation;

namespace Application.Members.Queries.GetById;

internal sealed class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdQueryValidator()
    {
        RuleFor(m => m.MemberId).NotEmpty().WithMessage("Member Id is required.");
    }
}
