using Microsoft.Extensions.Options;
using System.Text.Json;
using TaxCRM.Application.Infrastructure.ServiceBus;
using TaxCRM.Utils.Guards;

namespace TaxCRM.Application.Notifications.Mail;

public class ServiceBusMailService : IMailService
{
    private readonly ServiceBusOptions serviceBusOptions;
    private readonly SendGridOptions sendGridOptions;

    private readonly ServiceBusClientFactory factory;

    public ServiceBusMailService(ServiceBusClientFactory factory, IOptions<ServiceBusOptions> serviceBusOptions, IOptions<SendGridOptions> sendGridOptions)
    {
        Guard.ArgumentIsNotNull(serviceBusOptions.Value, nameof(serviceBusOptions.Value));
        this.serviceBusOptions = serviceBusOptions.Value;
        this.sendGridOptions = sendGridOptions.Value;

        this.factory = factory;
    }

    public async Task SendEntrepreneurProfileCreationEmail(CreationEmailMessage message)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(sendGridOptions.EntrepreneurProfileCreationTemplate));
        Guard.ArgumentIsNotNull(serviceBusOptions.MailQueue, nameof(serviceBusOptions.MailQueue));

        var msg = new { message.toEmail, message.fullName, message.country, templateId = sendGridOptions.EntrepreneurProfileCreationTemplate };

        var json = JsonSerializer.Serialize(msg);

        var client = factory.CreateClient(serviceBusOptions.MailQueue);
        await client.Send(json);
    }
}