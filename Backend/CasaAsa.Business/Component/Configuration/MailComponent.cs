using CasaAsa.Core.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace CasaAsa.Business.Component.Configuration
{
    public class MailComponent : IMailComponent
    {
        private readonly MailConfiguration _mailConfig;
        private readonly ILogger<MailComponent> _logger;

        public MailComponent(IOptions<MailConfiguration> mailConfig, ILogger<MailComponent> logger)
        {
            _mailConfig = mailConfig.Value;
            _logger = logger;
        }

        public void SendMail(Mail mail)
        {
            var smtpClient = new SmtpClient
            {
                Host = _mailConfig.Host,
                Port = _mailConfig.Port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_mailConfig.Username, _mailConfig.Password)
            };

            var email = new MailMessage
            {
                Subject = mail.Subject,
                Body = mail.Body,
                From = new MailAddress(mail.FromEmail, mail.SenderName),
                IsBodyHtml = true
            };

            email.To.Add(new MailAddress(mail.ToEmail, mail.ReceiverName));

            smtpClient.Send(email);

            _logger.LogInformation($"Email sent to {mail.ToEmail}...");
        }
    }
}
