namespace TaxCRM.Domain.Results;

//Add data to Result on error?
public class Result
{
    public bool Success { get; init; } = false;
    public Error? Error { get; init; }

    protected Result(Error error)
    {
        Success = false;
        Error = error;
    }

    protected Result()
    {
        Success = true;
        Error = default;
    }

    public static implicit operator Result(Error error) => new(error);
}


public sealed class Result<TValue> : Result
{
    public TValue? Data { get; init; } = default;

    private Result(TValue? data) : base()
    {
        Data = data;
    }

    private Result(Error error) : base(error)
    {
        Data = default;
    }

    public static implicit operator Result<TValue>(TValue data) => new(data);

    public static implicit operator Result<TValue>(Error error) => new(error);
}

