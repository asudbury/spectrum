namespace Spectrum.Application.Registration.Controllers
{
    using Core.Services;
    using Correspondence.Controllers;
    using Correspondence.Providers;
    using Model.Correspondence;
    using Model.Registration;
    using Providers;

    public class RegistrationController : EventController
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The registration provider.
        /// </summary>
        private readonly IRegistrationProvider registrationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="registrationProvider">The registration provider.</param>
        /// <param name="eventProvider">The event provider.</param>
        public RegistrationController(
            ISettingsService settingsService,
            IRegistrationProvider registrationProvider,
            IEventProvider eventProvider)
            : base(eventProvider)
        {
            this.settingsService = settingsService;
            this.registrationProvider = registrationProvider;
        }

        /// <summary>
        /// User has registered.
        /// </summary>
        /// <param name="model">The model.</param>
        public void UserRegistered(RegisterModel model)
        {
            if (settingsService.IsRegistrationEnabled)
            {
                registrationProvider.UserRegistered(model);

                EventModel eventModel = new EventModel(model.Guid, Event.UserRegistered);
                EventProvider.InsertEvent(eventModel);
            }
        }

        /// <summary>
        /// USer has been verified.
        /// </summary>
        /// <param name="model">The model.</param>
        public void UserVerified(NotificationModel model)
        {
            if (settingsService.IsRegistrationEnabled)
            {
                EventModel eventModel = new EventModel(model.Guid, Event.UserVerified);
                EventProvider.InsertEvent(eventModel);
            }
        }
    }
}
