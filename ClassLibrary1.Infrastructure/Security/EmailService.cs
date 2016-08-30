using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ClassLibrary1.Infrastructure.Security
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            // Example using SMTP client
            // TODO: move config to web config
            using (var client = new SmtpClient("127.0.0.1", 8000))
                using (var mailMessage = new MailMessage())
                {
                    mailMessage.Body = message.Body;
                    mailMessage.From = new MailAddress("my@app.com");
                    mailMessage.To.Add(message.Destination);
                    mailMessage.Subject = message.Subject;
                    mailMessage.IsBodyHtml = true;

                    await client.SendMailAsync(mailMessage);
                }


            // Plug in your email service here to send an email.
            //return Task.FromResult(0);
        }
    }
}