using Empo.Application.Configuration.Emails;

namespace Empo.Application.Configuration.Emails;

public interface IEmailSender
{
    Task SendEmailAsync(EmailMessage message);
}