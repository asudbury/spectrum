namespace Spectrum.Application.Correspondence.Repositories
{
    using Model.Correspondence;

    internal class EmailRepository : IEmailRepository
    {
        /// <summary>
        /// Email has been sent.
        /// </summary>
        /// <param name="model">The model.</param>
        public void EmailSent(EmailSentModel model)
        {
        }

        /// <summary>
        /// Email has been read.
        /// </summary>
        /// <param name="model">The model.</param>
        public void EmailRead(EmailReadModel model)
        {
        }
    }
}
