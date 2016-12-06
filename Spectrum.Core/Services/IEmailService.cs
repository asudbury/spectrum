namespace Spectrum.Core.Services
{
    using Model;

    public interface IEmailService
    {
        void SendEmail(EmailRequest emailRequest);
    }
}
