
namespace Spectrum.Content.Mail.Services
{
    using ContentModels;
    using Models;

    /// <summary>
    /// 
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        MailResponse SendEmail(MailTemplateModel model);
    }
}
