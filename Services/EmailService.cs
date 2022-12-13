using ContactPro.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ContactPro.Services
{
    public class EmailService : IEmailSender

    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailSender = _mailSettings.Email ?? Environment.GetEnvironmentVariable("Email");

            MimeMessage newEmail = new();

            newEmail.Sender = MailboxAddress.Parse(emailSender);

            foreach (var emailAddress in email.Split(';'))
            {
                newEmail.To.Add(MailboxAddress.Parse(emailAddress));
            }

            newEmail.Subject = subject;

            BodyBuilder emailBody = new();
            emailBody.HtmlBody = htmlMessage;

            newEmail.Body = emailBody.ToMessageBody();


            ///At this point lets log into our smtp client
            ///
            using SmtpClient stmpClient = new();

            try
            {
                var host = _mailSettings.EmailHost ?? Environment.GetEnvironmentVariable("EmailHost");
                var port = _mailSettings.EmailPort !=0 ? _mailSettings.EmailPort : int.Parse(Environment.GetEnvironmentVariable("EmailPort")!);
                var password = _mailSettings.EmailPassword ?? Environment.GetEnvironmentVariable("EmailPassword");

                await stmpClient.ConnectAsync(host, port, SecureSocketOptions.StartTls);
                await stmpClient.AuthenticateAsync(emailSender, password);

                await stmpClient.SendAsync(newEmail);
                await stmpClient.DisconnectAsync(true);

                
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw;
            }
        }
    }
}
