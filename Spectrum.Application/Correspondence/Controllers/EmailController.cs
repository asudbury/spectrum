namespace Spectrum.Application.Correspondence.Controllers
{
    using Core.Services;
    using Model.Correspondence;
    using Providers;

    public class EmailController : EventController
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The email provider.
        /// </summary>
        private readonly IEmailProvider emailProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailController" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="emailProvider">The email provider.</param>
        /// <param name="eventProvider">The event provider.</param>
        public EmailController(
            ISettingsService settingsService,
            IEmailProvider emailProvider,
            IEventProvider eventProvider)
            :base(eventProvider)
        {
            this.settingsService = settingsService;
            this.emailProvider = emailProvider;
        }

        /// <summary>
        /// The user has read an email.
        /// </summary>
        /// <param name="model">The model.</param>
        public void EmailRead(EmailReadModel model)
        {
            if (settingsService.IsCorrespondenceEnabled)
            {
                emailProvider.EmailRead(model);

                EventModel eventModel = new EventModel(model.Guid, Event.EmailRead);
                EventProvider.InsertEvent(eventModel);
            }
        }

        /// <summary>
        /// The user has been sent an email.
        /// </summary>
        /// <param name="model">The model.</param>
        public void EmailSent(EmailSentModel model)
        {
            if (settingsService.IsCorrespondenceEnabled)
            {
                emailProvider.EmailSent(model);

                EventModel eventModel = new EventModel(model.Guid, Event.EmailSent);
                EventProvider.InsertEvent(eventModel);
            }
        }
    }
}
