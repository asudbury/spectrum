namespace Spectrum.Content.Mail.Services
{
    using ContentModels;
    using Models;
    using System;
    using System.Net.Mail;

    public class MailService : IMailService
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public MailResponse SendEmail(MailTemplateModel model)
        {
            if (string.IsNullOrEmpty(model.From))
            {
                throw new ArgumentException("From address is null");
            }

            if (string.IsNullOrEmpty(model.To))
            {
                throw new ArgumentException("To address is null");
            }
            
            MailResponse response = new MailResponse
            {
                Contents = model.TokenizedText,
                Sent = true,
                ToEmailAddress = model.To,
                Surpressed = true //// will be overridden below
            };

            if (model.SurpressSendEmail == false)
            {
                response.Surpressed = false;

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(model.From),
                    Subject = model.Subject,
                    Body = model.TokenizedText,
                    IsBodyHtml = model.IsHtml
                };

                string[] emailto = model.To.Split(';');

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
