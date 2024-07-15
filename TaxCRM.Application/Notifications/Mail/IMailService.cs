namespace TaxCRM.Application.Notifications.Mail;

public interface IMailService
{
    Task SendEntrepreneurProfileCreationEmail(CreationEmailMessage message);
}
