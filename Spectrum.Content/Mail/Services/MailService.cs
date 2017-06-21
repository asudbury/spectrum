namespace Spectrum.Content.Mail.Services
{
    using ContentModels;
    using Models;
    using System.Net.Mail;

    public class MailService : IMailService
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public MailResponse SendEmail(
            string to, 
            MailTemplateModel model)
        {
            MailResponse response = new MailResponse
            {
                Contents = model.TokenizedText,
                Sent = true,
                ToEmailAddress = to
            };

            if (model.SurpressSendEmail == false)
            {
                MailMessage mailMessage = new MailMessage
                {
                    Body = model.Text
                };

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(mailMessage);
            }

            return response;
        }
    }
}
