using Azure.Messaging.ServiceBus;

namespace TaxCRM.Application.Infrastructure.ServiceBus;

//Add interface?
public class ServiceBusQueueClient
{
    private readonly ServiceBusSender sender;

    public ServiceBusQueueClient(ServiceBusSender sender)
    {
        this.sender = sender;
    }

    public async Task Send(string message)
    {
        var serviceBusMessage = new ServiceBusMessage(message);
        await sender.SendMessageAsync(serviceBusMessage);
    }
}
