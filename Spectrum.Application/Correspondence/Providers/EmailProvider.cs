
namespace Spectrum.Application.Correspondence.Providers
{
    using Core.Services;
    using Model.Correspondence;
    using Repositories;

    /// <summary>
    /// The EmailProvider class.
    /// </summary>
    internal class EmailProvider : IEmailProvider
    {
        /// <summary>
        /// The email repository.
        /// </summary>
        private readonly IEmailRepository emailRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailProvider" /> class.
        /// </summary>
        /// <param name="emailRepository">The customer repository.</param>
        internal EmailProvider(IEmailRepository emailRepository)
        {
            this.emailRepository = emailRepository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailProvider"/> class.
        /// </summary>
        internal EmailProvider()
            : this(new EmailRepository(new DatabaseService()))
        {
        }

        /// <summary>
        /// Email has been sent.
        /// </summary>
        /// <param name="model">The model.</param>
        public void EmailSent(EmailSentModel model)
        {
            emailRepository.EmailSent(model);
        }

        /// <summary>
        /// Email has been read.
        /// </summary>
        /// <param name="model">The model.</param>
        public void EmailRead(EmailReadModel model)
        {
            emailRepository.EmailRead(model);
        }
    }
}
