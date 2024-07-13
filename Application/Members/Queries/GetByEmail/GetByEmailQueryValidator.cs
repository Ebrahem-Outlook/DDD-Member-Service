using FluentValidation;

namespace Application.Members.Queries.GetByEmail;

internal sealed class GetByEmailQueryValidator : AbstractValidator<GetByEmailQuery>
{
    public GetByEmailQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
    }
}
