using TaxCRM.Domain.Common;

namespace TaxCRM.Domain.Entrepreneurs;

public class Entrepreneur : Entity
{
    public string FirstName { get; init; } = String.Empty;
    public string LastName { get; init; } = String.Empty;
    public string Email { get; init; } = String.Empty;
}
