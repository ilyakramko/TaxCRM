using Microsoft.Extensions.DependencyInjection;
using TaxCRM.Application.Entrepreneurs;
using TaxCRM.Application.Incomes;
using TaxCRM.Application.Infrastructure;

namespace TaxCRM.Application;

public static class ServiceRegistration
{
    public static void RegisterApplication(this IServiceCollection serviceCollection)
    {
        MapsterConfig.Configure();

        serviceCollection.AddScoped<EntrepreneurService>();
        serviceCollection.AddScoped<IncomeService>();
    }
}
