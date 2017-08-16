namespace Spectrum.Content.Mail.Services
{
    using ContentModels;
    using Ical.Net.Serialization;
    using Ical.Net.Serialization.iCalendar.Serializers;
    using Models;
    using System.IO;
    using System.Net.Mail;
    using System.Text;

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
                    To = { mailTo },
                    From = new MailAddress(model.From),
                    Subject = model.Subject,
                    Body = model.TokenizedText
                };

                if (string.IsNullOrEmpty(model.BlindCopy) == false)
                {
                    mailMessage.Bcc.Add(new MailAddress(model.BlindCopy));
                }

                if (model.Attachment != null)
                {
                    CalendarSerializer serializer = new CalendarSerializer(new SerializationContext());
                    string serializedAttachment = serializer.SerializeToString(model.Attachment.Data);
                    byte[] bytesAttachment = Encoding.UTF8.GetBytes(serializedAttachment);

                    MemoryStream ms = new MemoryStream(bytesAttachment);
                    mailMessage.Attachments.Add(new Attachment(ms, model.Attachment.FileName, model.Attachment.MimeType));
                }

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(mailMessage);
            }

            return response;
        }
    }
}
