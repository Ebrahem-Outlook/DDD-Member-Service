using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;

namespace Application.Core.Exceptions;

internal sealed class ValidationExceptoin : Exception
{
    public ValidationException(IEnumerable<ValidationFailure> failures)
        : base() =>
        ;


    public IReadOnlyCollection<Error>


}
