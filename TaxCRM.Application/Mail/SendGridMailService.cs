using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace TaxCRM.Application.Mail;

public class SendGridMailService : IMailService
{
    private readonly SendGridClient client;
    private readonly SendGridOptions options;

    public SendGridMailService(IOptions<SendGridOptions> options)
    {
        this.options = options.Value;
        ArgumentException.ThrowIfNullOrEmpty(nameof(this.options.ApiKey));
        ArgumentException.ThrowIfNullOrEmpty(nameof(this.options.FromEmail));

        client = new SendGridClient(this.options.ApiKey);
    }

    public async Task SendEntrepreneurProfileCreationEmail(string toEmail, string fullName, string country)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(options.EntrepreneurProfileCreationTemplate));

        var from = new EmailAddress(options.FromEmail);
        var to = new EmailAddress(toEmail);

        var msg = MailHelper.CreateSingleTemplateEmail(from, to, options.EntrepreneurProfileCreationTemplate, new { Name = fullName, Country = country });

        await client.SendEmailAsync(msg);
    }
}

public class SendGridOptions
{
    public string? ApiKey { get; set; }
    public string? FromEmail { get; set; }
    public string? EntrepreneurProfileCreationTemplate { get; set; }
}