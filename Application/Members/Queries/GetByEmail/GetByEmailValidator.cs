using FluentValidation;

namespace Application.Members.Queries.GetByEmail;

internal sealed class GetByEmailValidator : AbstractValidator<GetByEmailQuery>
{
    public GetByEmailValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
    }
}
