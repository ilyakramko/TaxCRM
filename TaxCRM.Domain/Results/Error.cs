namespace TaxCRM.Domain.Results;

public record Error(string Code, ErrorType ErrorType, string Message);

public enum ErrorType
{
    Argument,
    NotFound,

    Empty
}
