using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
namespace SmartBudget.Services
    {
    public class EmailService : IEmailSender
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings) // Inject IOptions to get SmtpSettings
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Use the provided SMTP client to send the email
            using (var smtpClient = new SmtpClient(_smtpSettings.Host))
            {
                smtpClient.Port = _smtpSettings.Port;
                smtpClient.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
                smtpClient.EnableSsl = true;  // Enable SSL for secure email transmission

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.From, _smtpSettings.FromName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true // You can send HTML-formatted emails
                };

                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }

        public async Task SendMfaTokenAsync(string email, string token)
        {
            var subject = "Your MFA Code";
            var message = $"Your MFA code is: {token}. Please use this code to verify your login.";
            await SendEmailAsync(email, subject, message);
        }
    }

    public class SmtpSettings
    {
        public string Host { get; set; }      // SMTP Server Address
        public int Port { get; set; }         // SMTP Port (587, 465, or 25)
        public string Username { get; set; }  // SMTP Username (usually the email address)
        public string Password { get; set; }  // SMTP Password
        public string From { get; set; }      // "From" email address
        public string FromName { get; set; }  // "From" name
    }

}
