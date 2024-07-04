namespace TaxCRM.Domain.Results;

//Add data to Result on error?
public class Result
{
    public bool Success { get; init; } = false;
    public Error Error { get; init; }

    protected Result(bool success, Error? error)
    {
        Success = success;
        Error = error ?? Errors.Errors.Common.Empty;
    }

    public static Result FromSuccess() =>
        new Result(true, null);

    public static Result FromFailure(Error error) =>
        new Result(false, error);
}


public sealed class Result<T> : Result
{
    public T? Data { get; init; } = default;

    private Result(T? data, bool success, Error? error) : base(success, error)
    {
        Data = data;
    }

    public static Result<T> FromSuccess(T data) =>
        new Result<T>(data, true, null);

    public static new Result<T> FromFailure(Error error) =>
        new Result<T>(default, false, error);
}

