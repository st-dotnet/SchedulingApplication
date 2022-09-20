using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using SchedulingApplication.Infrastructure.Interface;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace SchedulingApplication.Infrastructure.Services
{
    public class EmailServices : IEmailServices
    {
        private IOptions<AppSettings> _appSettings;

        public EmailServices(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public void Send(string to, string subject, string html, string from = null)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? _appSettings.Value.SmtpSettings.Email));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_appSettings.Value.SmtpSettings.Server, (int)_appSettings.Value.SmtpSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_appSettings.Value.SmtpSettings.EmailAddressOrders, _appSettings.Value.SmtpSettings.SmtpPasswordOrders);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
