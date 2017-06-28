namespace Spectrum.Application.Authentication.Controllers
{
    using Core.Services;
    using Correspondence.Controllers;
    using Correspondence.Providers;
    using Model.Correspondence;

    public class PasswordController : EventController
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordController" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="eventProvider">The event provider.</param>
        public PasswordController(
            ISettingsService settingsService,
            IEventProvider eventProvider)
            :base(eventProvider)
        {
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Password reset requested.
        /// </summary>
        /// <param name="model">The model.</param>
        public void ResetRequested(NotificationModel model)
        {
            if (settingsService.IsAuthenticationEnabled)
            {
                EventModel eventModel = new EventModel(model.Guid, Event.PasswordResetRequested);
                EventProvider.InsertEvent(eventModel);
            }
        }

        /// <summary>
        /// Password Reset Completed.
        /// </summary>
        /// <param name="model">The model.</param>
        public void ResetCompleted(NotificationModel model)
        {
            if (settingsService.IsAuthenticationEnabled)
            {
                EventModel eventModel = new EventModel(model.Guid, Event.PasswordResetCompleted);
                EventProvider.InsertEvent(eventModel);
            }
        }
    }
}
