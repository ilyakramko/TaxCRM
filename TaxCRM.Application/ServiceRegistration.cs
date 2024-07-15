using Microsoft.Extensions.DependencyInjection;
using TaxCRM.Application.Entrepreneurs;
using TaxCRM.Application.Incomes;
using TaxCRM.Application.Infrastructure;
using TaxCRM.Application.Infrastructure.ServiceBus;
using TaxCRM.Application.Notifications;
using TaxCRM.Application.Notifications.Mail;

namespace TaxCRM.Application;

public static class ServiceRegistration
{
    public static void RegisterApplication(this IServiceCollection serviceCollection)
    {
        MapsterConfig.Configure();

        serviceCollection.AddSingleton<ServiceBusClientFactory>();

        serviceCollection.AddScoped<NotificationService>();

        serviceCollection.AddScoped<EntrepreneurService>();
        serviceCollection.AddScoped<IncomeService>();
        //serviceCollection.AddScoped<IMailService, SendGridMailService>();
        serviceCollection.AddScoped<IMailService, ServiceBusMailService>();
    }
}
