namespace TaxCRM.Application.Mail;

public interface IMailService
{
    public Task SendEntrepreneurProfileCreationEmail(string toEmail, string fullName, string country);
}
