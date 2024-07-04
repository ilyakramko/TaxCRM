using Mapster;
using TaxCRM.Application.Incomes;
using TaxCRM.Domain.Incomes;

namespace TaxCRM.Application.Infrastructure;

public static class MapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<Income, IncomeView>.NewConfig()
            .Map(dest => dest.Amount, src => src.Value.Amount)
            .Map(dest => dest.Currency, src => src.Value.Currency);
    }
}
