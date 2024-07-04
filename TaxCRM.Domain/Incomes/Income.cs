using TaxCRM.Domain.Common;

namespace TaxCRM.Domain.Incomes;

public class Income : Entity
{
    public Money Value { get; init; } = new Money(0, Currency.USD);
    public DateTime Date { get; init; }

    public Guid EntrepreneurProfileId { get; set; }
}
