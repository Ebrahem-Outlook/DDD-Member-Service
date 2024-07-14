namespace Domain.Core.BaseType.Result;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if(isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFaiuler => !IsSuccess;

    public Error Error { get; }


    public static Result Success() => new Result(true, Error.None);

    public static Result Failure(Error error) => new Result(false, error);


    public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value, true, Error.None);

    public static Result<TValue> Failure<TValue>(TValue value, Error error) => new Result<TValue>(value, false, error);


    public static Result<TValue> Create<TValue>(TValue value, Error error) => value is null || error != Error.None
        ? Failure<TValue>(value, error)
        : Success<TValue>(value);

    public static Result FirstFailureOrSuccess(params Result[] results)
    {
        foreach (Result result in results)
        {
            if (result.IsFaiuler)
            {
                return result;
            }
        }

        return Success();
    }
}


