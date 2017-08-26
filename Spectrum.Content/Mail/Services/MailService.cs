namespace Spectrum.Content.Mail.Services
{
    using ContentModels;
    using Models;
    using System.Net.Mail;

    public class MailService : IMailService
    {
        /// <inheritdoc />
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
            string mailTo = model.To;

            if (string.IsNullOrEmpty(mailTo))
            {
                mailTo = to;
            }
          
            MailResponse response = new MailResponse
            {
                Contents = model.TokenizedText,
                Sent = true,
                ToEmailAddress = mailTo
            };

            if (model.SurpressSendEmail == false)
            {
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(model.From),
                    Subject = model.Subject,
                    Body = model.TokenizedText,
                    IsBodyHtml = model.IsHtml
                };

                string[] emailto = mailTo.Split(';');

                foreach (string mail in emailto)
                {
                    if (mail != string.Empty)
                    {
                        mailMessage.To.Add(mail);
                    }
                }

                if (string.IsNullOrEmpty(model.BlindCopy) == false)
                { 
                    string[] emailBcc = model.BlindCopy.Split(';');

                    foreach (string mail in emailBcc)
                    {
                        if (mail != string.Empty)
                        {
                            mailMessage.Bcc.Add(mail);
                        }
                    }
                }

                if (model.Attachments != null)
                {
                    foreach (Attachment attachment in model.Attachments)
                    {
                        mailMessage.Attachments.Add(attachment);
                    }    
                }

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(mailMessage);
            }

            return response;
        }
    }
}
