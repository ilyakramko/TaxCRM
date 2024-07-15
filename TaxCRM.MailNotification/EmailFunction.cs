using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaxCRM.MailNotification;

public class EmailFunction
{
    private readonly JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    private readonly string apiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
    private readonly string fromEmail = Environment.GetEnvironmentVariable("SendGridFromEmail");

    [FunctionName("SendEmailFunction")]
    public async Task Run([ServiceBusTrigger("%MailQueue%", Connection = "ServiceBusConnection")] string myQueueItem, ILogger log)
    {
        var message = JsonSerializer.Deserialize<CreationEmailMessage>(myQueueItem, options);

        var client = new SendGridClient(apiKey); 
        
        var from = new EmailAddress(fromEmail);
        var to = new EmailAddress(message.ToEmail);

        var msg = MailHelper.CreateSingleTemplateEmail(from, to, message.TemplateId, new { Name = message.FullName, Country = message.Country });

        await client.SendEmailAsync(msg);
    }
}

public class CreationEmailMessage
{
    public string ToEmail { get; set; }
    public string FullName { get; set; }
    public string Country { get; set; }
    public string TemplateId { get; set; }
}
