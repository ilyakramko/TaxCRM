namespace TaxCRM.Domain.Results.Errors;

public static partial class Errors
{
    public static class Entrepreneur
    {
        private static string CodePrefix = "Entrepreneur";

        public static Error NotFound = new Error($"{CodePrefix}.NotFound", ErrorType.NotFound, "Entrepreneur for provided Id was not found");
    }

    public static class EntrepreneurProfile
    {
        private static string CodePrefix = "EntrepreneurProfile";

        public static Error InvalidTaxPayerNumber = new Error($"{CodePrefix}.InvalidTaxPayerNumber", ErrorType.Argument, "The taxpayer number invalid for selected country");
        public static Error CountryIdNotValid = new Error($"{CodePrefix}.CountryIdNotValid", ErrorType.Argument, "The country identifier isn't valid");

        public static Error NotFound = new Error($"{CodePrefix}.NotFound", ErrorType.NotFound, "Entrepreneur profile for provided Id was not found");
    }
}
