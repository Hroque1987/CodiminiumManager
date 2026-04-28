using Sharedkernel.Errors;

namespace Sharedkernel.Results;

public sealed class Result<T>
{
    private readonly T _value;

    public T Value
    {
        get
        {
            if (IsFailure)
                throw new InvalidOperationException("Cannot access Value on failure.");
            return _value;
        }
    }

    public IReadOnlyList<Error> Errors { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    private Result(T value)
    {
        _value = value;
        Errors = [];
        IsSuccess = true;
    }

    private Result(List<Error> errors)
    {
        _value = default!;
        Errors = errors;
        IsSuccess = false;
    }

    public static Result<T> Success(T value) => new(value);

    public static Result<T> Failure(List<Error> errors) => new(errors);

    public static Result<T> Failure(Error error)
        => new([error]);

    

}