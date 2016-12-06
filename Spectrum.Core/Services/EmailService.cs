namespace Spectrum.Core.Services
{
    using Model;
    using System.Net.Mail;

    public class EmailService : IEmailService
    {
        public void SendEmail(EmailRequest emailRequest)
        {
            SmtpClient client = new SmtpClient
            {
                Host = emailRequest.Server,
            };

            client.Send(emailRequest.Message);
        }
    }
}
