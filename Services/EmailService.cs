using CubitsApp.Models;
using Microsoft.Extensions.Options;
using static System.Net.WebRequestMethods;
using System.Net.Mail;
using System.Net;

namespace CubitsApp.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public void SendEmail(string to, string otp = "")
        {
            var smtpServer = _emailSettings.SMTPServer;
            var smtpPort = _emailSettings.SMTPPort;
            var smtpUsername = _emailSettings.SMTPUser;
            var smtpPassword = _emailSettings.SMTPPassword;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("testing@example.com", "Hi");
            mail.To.Add(to);
            mail.Subject = "Your OTP Code";
            mail.Body = $"Your OTP code is {otp}.";

            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };

            try
            {
                smtpClient.Send(mail);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
