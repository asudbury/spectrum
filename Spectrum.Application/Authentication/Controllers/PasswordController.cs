namespace Spectrum.Application.Authentication.Controllers
{
    using Correspondence.Controllers;
    using Correspondence.Providers;
    using Model.Correspondence;

    public class PasswordController : EventController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordController"/> class.
        /// </summary>
        /// <param name="passwordProvider">The password provider.</param>
        /// <param name="eventProvider">The event provider.</param>
        public PasswordController(IEventProvider eventProvider)
            :base(eventProvider)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordController" /> class.
        /// </summary>
        public PasswordController()
            : this(new EventProvider())
        {
            
        }
        /// <summary>
        /// Password reset requested.
        /// </summary>
        /// <param name="model">The model.</param>
        public void ResetRequested(NotificationModel model)
        {
            EventModel eventModel = new EventModel(model.Guid, Event.PasswordResetRequested);
            eventProvider.InsertEvent(eventModel);
        }

        /// <summary>
        /// Password Reset Completed.
        /// </summary>
        /// <param name="model">The model.</param>
        public void ResetCompleted(NotificationModel model)
        {
            EventModel eventModel = new EventModel(model.Guid, Event.PasswordResetCompleted);
            eventProvider.InsertEvent(eventModel);
        }
    }
}
