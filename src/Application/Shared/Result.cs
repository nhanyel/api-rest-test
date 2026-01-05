using System;

namespace Application.Shared;

public class Result<T>
{
    public T? Value { get; }
    public Failure? Error { get; }
    public bool IsSuccess => Error == null;

    private Result(T value)
    {
        Value = value;
        Error = null;
    }

    private Result(Failure error)
    {
        Error = error;
        Value = default;
    }

    public static Result<T> Ok(T value) => new Result<T>(value);

    public static Result<T> Fail(Failure error) => new Result<T>(error); // ✅ CORRECTO
}
