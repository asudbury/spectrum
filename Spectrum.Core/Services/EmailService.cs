namespace Spectrum.Core.Services
{
    using Model.Correspondence;
    using System.Net.Mail;

    /// <summary>
    /// The EmailService class.
    /// </summary>
    /// <seealso cref="Spectrum.Core.Services.IEmailService" />
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="emailRequest">The email request.</param>
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
