namespace Spectrum.Application.Correspondence.Controllers
{
    using Model.Correspondence;
    using Providers;

    public class EmailController : EventController
    {
        /// <summary>
        /// The email provider.
        /// </summary>
        private readonly IEmailProvider emailProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailController" /> class.
        /// </summary>
        /// <param name="emailProvider">The email provider.</param>
        public EmailController(
            IEmailProvider emailProvider,
            IEventProvider eventProvider)
            :base(eventProvider)
        {
            this.emailProvider = emailProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailController"/> class.
        /// </summary>
        public EmailController()
            : this(new EmailProvider(), new EventProvider())
        {
        }

        /// <summary>
        /// The user has read an email.
        /// </summary>
        /// <param name="model">The model.</param>
        public void EmailRead(EmailReadModel model)
        {
            emailProvider.EmailRead(model);

            EventModel eventModel = new EventModel(model.Guid, Event.EmailRead);
            eventProvider.InsertEvent(eventModel);
        }

        /// <summary>
        /// The user has been sent an email.
        /// </summary>
        /// <param name="model">The model.</param>
        public void EmailSent(EmailSentModel model)
        {
            emailProvider.EmailSent(model);

            EventModel eventModel = new EventModel(model.Guid, Event.EmailSent);
            eventProvider.InsertEvent(eventModel);
        }
    }
}
