namespace TaxCRM.Domain.Results.Errors;

public static partial class Errors
{
    public static class Income
    {
        private static string CodePrefix = "Income";

        public static Error AmountAboveZero = new Error($"{CodePrefix}.AmountAboveZero", ErrorType.Argument, "The Amount should be above the 0");
        public static Error CurrencyIdNotValid = new Error($"{CodePrefix}.CurrencyIdNotValid", ErrorType.Argument, "The currency identifier isn't valid");
        public static Error IncomeDate = new Error($"{CodePrefix}.IncomeDate", ErrorType.Argument, "The income date should be in the past");

        public static Error NotFound = new Error($"{CodePrefix}.NotFound", ErrorType.NotFound, "Income for provided Id was not found");
    }
}
