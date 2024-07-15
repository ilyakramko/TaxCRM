using TaxCRM.Application.Notifications.Mail;

namespace TaxCRM.Application.Notifications;

public class NotificationService(IMailService mailService)
{
    public async Task SendEntrepreneurProfileCreationNotification(string toEmail, string fullName, string country, NotificationType notificationType = NotificationType.Email)
    {
        if (notificationType == NotificationType.Email)
            await mailService.SendEntrepreneurProfileCreationEmail(new CreationEmailMessage(toEmail, fullName, country));
    }
}

public enum NotificationType
{
    Email = 0
}
