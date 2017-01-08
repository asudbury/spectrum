namespace Spectrum.Application.Correspondence.Providers
{
    using Model.Correspondence;

    /// <summary>
    /// The IEmailProvider interface.
    /// </summary>
    public interface IEmailProvider
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
