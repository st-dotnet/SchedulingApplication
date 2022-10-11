using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using SchedulingApplication.Infrastructure.Interface;
using System.Net.Mail;
using MailKit.Security;
using FluentEmail.Core;
using System.Net;

namespace SchedulingApplication.Infrastructure.Services
{
    public class EmailServices : IEmailServices
    {
        private IOptions<AppSettings> _appSettings;
		private IOptions<MailKitEmailSenderOptions> _options;


		public EmailServices(IOptions<AppSettings> appSettings, IOptions<MailKitEmailSenderOptions> options)
        {
            _appSettings = appSettings;
			_options = options;
        }

		public MailKitEmailSenderOptions Options { get; set; }

		public bool Send(string to, string subject, string html, string from = null)
		{
			try
			{
				int port = 587;
				string host = "smtp.office365.com";
				string username = "mohit.saini@supremetechnologiesindia.com";
				string password = "Sot88670";
				string mailFrom = "mohit.saini@supremetechnologiesindia.com";
				string mailTo = to;
				string mailTitle = subject;
				string mailMessage = html;
				var message = new MimeMessage();
				message.From.Add(new MailboxAddress(mailFrom, mailFrom));
				message.To.Add(new MailboxAddress(mailTo, mailTo));
				message.Subject = mailTitle;
				message.Body = new TextPart("html") { Text = mailMessage };
				using (var client = new MailKit.Net.Smtp.SmtpClient())
				{
					client.Connect(host, port, false);
					client.Authenticate(username, password);
					client.Send(message);
					client.Disconnect(true);
				}

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
    }
}
