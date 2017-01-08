namespace Spectrum.Application.Correspondence.Repositories
{
    using Model.Correspondence;

    public interface IEmailRepository
    {
        /// <summary>
        /// Email has been sent.
        /// </summary>
        /// <param name="model">The model.</param>
        void EmailSent(EmailSentModel model);

        /// <summary>
        /// Email has been read.
        /// </summary>
        /// <param name="model">The model.</param>
        void EmailRead(EmailReadModel model);
    }
}
