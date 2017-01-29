namespace Spectrum.Application.Authentication.Controllers
{
    using Core.Services;
    using Correspondence.Controllers;
    using Correspondence.Providers;
    using Model.Correspondence;

    public class LoginController : EventController
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="eventProvider">The event provider.</param>
        public LoginController(
            ISettingsService settingsService,
            IEventProvider eventProvider)
            : base(eventProvider)
        {
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        public LoginController()
            : this(new SettingsService(), new EventProvider())
        {
        }

        /// <summary>
        /// The user has logged in.
        /// </summary>
        /// <param name="model">The model.</param>
        public void LoginComplete(NotificationModel model)
        {
            if (settingsService.IsAuthenticationEnabled)
            {
                EventModel eventModel = new EventModel(model.Guid, Event.LoginComplete);
                EventProvider.InsertEvent(eventModel);
            }
        }

        /// <summary>
        /// The user login has failed.
        /// </summary>
        /// <param name="model">The model.</param>
        public void LoginFailed(NotificationModel model)
        {
            if (settingsService.IsAuthenticationEnabled)
            {
                EventModel eventModel = new EventModel(model.Guid, Event.LoginFailed);
                EventProvider.InsertEvent(eventModel);
            }
        }
    }
}
