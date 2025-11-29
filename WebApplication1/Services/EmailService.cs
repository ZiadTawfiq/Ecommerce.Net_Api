
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using WebApplication1.Entities;

namespace WebApplication1.Services
{
    public class EmailService(IOptions<MailSettings>options) : IEmailService
    {
  
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var settings = options.Value;
            var smtpClient = new SmtpClient(settings.Host)
            {
                Port = 587,
                Credentials = new NetworkCredential(settings.Mail, settings.Password),
                EnableSsl = settings.EnableSSL,

            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(settings.Mail, settings.DisplayName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };


            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);

            
            


        }
    }
}
