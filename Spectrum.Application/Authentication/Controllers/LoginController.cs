namespace Spectrum.Application.Authentication.Controllers
{
    using Correspondence.Controllers;
    using Correspondence.Providers;
    using Model.Correspondence;

    public class LoginController : EventController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController" /> class.
        /// </summary>
        /// <param name="loginProvider">The login provider.</param>
        public LoginController(IEventProvider eventProvider)
            : base(eventProvider)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        public LoginController()
            : this(new EventProvider())
        {
        }

        /// <summary>
        /// The user has logged in.
        /// </summary>
        /// <param name="model">The model.</param>
        public void LoginComplete(NotificationModel model)
        {
            EventModel eventModel = new EventModel(model.Guid, Event.LoginComplete);
            eventProvider.InsertEvent(eventModel);
        }

        /// <summary>
        /// The user login has failed.
        /// </summary>
        /// <param name="model">The model.</param>
        public void LoginFailed(NotificationModel model)
        {
            EventModel eventModel = new EventModel(model.Guid, Event.LoginFailed);
            eventProvider.InsertEvent(eventModel);
        }
    }
}
