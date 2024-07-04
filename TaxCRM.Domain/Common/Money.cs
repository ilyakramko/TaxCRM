namespace TaxCRM.Domain.Common;

public record Money(decimal Amount, Currency Currency);

public enum Currency 
{
    USD,
    EUR,
    GBR,
    GEL
}

