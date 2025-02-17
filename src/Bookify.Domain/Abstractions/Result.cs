using System.Diagnostics.CodeAnalysis;

namespace Bookify.Domain.Abstractions;

public class Result
{
    protected internal Result(bool isSuccess, Errors error)
    {
        if (isSuccess && error != Errors.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Errors.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Errors Error { get; }

    public static Result Success() => new(true, Errors.None);

    public static Result Failure(Errors error) => new(false, error);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Errors.None);

    public static Result<TValue> Failure<TValue>(Errors error) => new(default, false, error);

    public static Result<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Errors.NullValue);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, Errors error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}