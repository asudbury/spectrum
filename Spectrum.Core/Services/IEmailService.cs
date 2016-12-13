namespace Spectrum.Core.Services
{
    using Model;

    /// <summary>
    /// The EmailService interface.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="emailRequest">The email request.</param>
        void SendEmail(EmailRequest emailRequest);
    }
}
