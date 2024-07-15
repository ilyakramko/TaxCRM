using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using TaxCRM.Utils.Guards;

namespace TaxCRM.Application.Infrastructure.ServiceBus;

public class ServiceBusClientFactory
{
    private readonly ServiceBusClient client;

    public ServiceBusClientFactory(IOptions<ServiceBusOptions> options)
    {
        Guard.ArgumentIsNotNull(options.Value, nameof(options.Value));
        Guard.ArgumentIsNotNull(options.Value.ConnectionString, nameof(options.Value.ConnectionString));

        client = new ServiceBusClient(options.Value.ConnectionString);
    }

    public ServiceBusQueueClient CreateClient(string queueName) =>
        new ServiceBusQueueClient(client.CreateSender(queueName));
}
