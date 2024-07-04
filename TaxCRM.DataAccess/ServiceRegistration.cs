using Microsoft.Extensions.DependencyInjection;
using TaxCRM.DataAccess.Entrepreneurs;
using TaxCRM.DataAccess.Incomes;
using TaxCRM.Domain.Entrepreneurs;
using TaxCRM.Domain.Incomes;

namespace TaxCRM.DataAccess;

public static class ServiceRegistration
{
    public static void RegisterDataAccess(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IEntrepreneurRepository, EntrepreneurRepository>();
        serviceCollection.AddScoped<IEntrepreneurProfileRepository, EntrepreneurProfileRepository>();
        serviceCollection.AddScoped<IIncomeRepository, IncomeRepository>();
    }
}
