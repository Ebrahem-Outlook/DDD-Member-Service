using FluentValidation;

namespace Application.Members.Queries.GetByName;

internal sealed class GetByNameQueryValidator : AbstractValidator<GetByNameQuery>
{
    public GetByNameQueryValidator()
    {
        RuleFor(m => m.Name).NotEmpty().WithMessage("Member Name is required.");
    }
}
