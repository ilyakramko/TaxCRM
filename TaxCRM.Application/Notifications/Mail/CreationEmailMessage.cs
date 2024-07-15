namespace TaxCRM.Application.Notifications.Mail;

public record CreationEmailMessage(string toEmail, string fullName, string country);
