namespace TaxCRM.Application.Infrastructure.ServiceBus;

public class ServiceBusOptions
{
    public string? ConnectionString { get; set; }
    public string? MailQueue { get; set; }
}
