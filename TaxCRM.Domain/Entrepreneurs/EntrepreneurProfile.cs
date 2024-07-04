using TaxCRM.Domain.Common;
using TaxCRM.Domain.Results;
using TaxCRM.Domain.Results.Errors;
using TaxCRM.Utils;

namespace TaxCRM.Domain.Entrepreneurs;

public class EntrepreneurProfile : Entity
{
    public string TaxPayerNumber { get; init; } = String.Empty;
    public Country Country { get; init; }

    public Guid EntrepreneurId { get; set; }

    private EntrepreneurProfile(Country country, string taxPayerNumber, Guid entrepreneurId)
    {
        TaxPayerNumber = taxPayerNumber;
        Country = country;
        EntrepreneurId = entrepreneurId;
    }

    public static Result<EntrepreneurProfile> Create(string country, string taxPayerNumber, Guid entrepreneurId)
    {
        if (!EnumEx.TryValidate<Country>(country, out var parsedCountry))
            return Result<EntrepreneurProfile>.FromFailure(Errors.EntrepreneurProfile.CountryIdNotValid);

        var result = parsedCountry switch
        {
            Country.GE => taxPayerNumber.Length == 9 && taxPayerNumber.All(char.IsNumber),
            _ => false
        };

        if (!result)
            return Result<EntrepreneurProfile>.FromFailure(Errors.EntrepreneurProfile.InvalidTaxPayerNumber);

        return Result<EntrepreneurProfile>.FromSuccess(new EntrepreneurProfile(parsedCountry, taxPayerNumber, entrepreneurId));
    }
}


