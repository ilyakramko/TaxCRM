using TaxCRM.Domain.Common;
using TaxCRM.Domain.Results;
using TaxCRM.Domain.Results.Errors;
using TaxCRM.Utils;

namespace TaxCRM.Domain.Incomes;

public class Income : Entity
{
    public Money Value { get; init; } = new Money(0, Currency.USD);
    public DateTime Date { get; init; }

    public Guid EntrepreneurProfileId { get; set; }

    private Income(decimal amount, Currency currency, DateTime utcDate, Guid entrepreneurProfileId)
    {
        Value = new Money(amount, currency);
        Date = utcDate;
        EntrepreneurProfileId = entrepreneurProfileId;
    }

    public static Result<Income> Create(decimal amount, string currency, DateTime date, Guid entrepreneurProfileId)
    {
        //Use DateTime Provider		
        var now = DateTime.UtcNow;
        var utcDate = date.ToUniversalTime();

        if (amount <= 0)
            return Errors.Income.AmountAboveZero;

        if (!EnumEx.TryValidate<Currency>(currency, out var parsedCurrency))
            return Errors.Income.CurrencyIdNotValid;

        if (utcDate > now)
            return Errors.Income.IncomeDate;

        return new Income(amount, parsedCurrency, utcDate, entrepreneurProfileId);
    }
}
