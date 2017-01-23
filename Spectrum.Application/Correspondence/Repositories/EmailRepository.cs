namespace Spectrum.Application.Correspondence.Repositories
{
    using Core.Services;
    using Model.Correspondence;

    internal class EmailRepository : IEmailRepository
    {
        /// <summary>
        /// The database service.
        /// </summary>
        private readonly IDatabaseService databaseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailRepository"/> class.
        /// </summary>
        /// <param name="databaseService">The database service.</param>
        public EmailRepository(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

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
