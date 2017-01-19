namespace Spectrum.Application.Registration.Controllers
{
    using Correspondence.Controllers;
    using Correspondence.Providers;
    using Model.Correspondence;
    using Model.Registration;
    using Providers;

    public class RegistrationController : EventController
    {
        /// <summary>
        /// The registration provider.
        /// </summary>
        private readonly IRegistrationProvider registrationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController" /> class.
        /// </summary>
        /// <param name="registrationProvider">The registration provider.</param>
        /// <param name="eventProvider">The event provider.</param>
        public RegistrationController(
            IRegistrationProvider registrationProvider,
            IEventProvider eventProvider)
            : base(eventProvider) 
        {
            this.registrationProvider = registrationProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController"/> class.
        /// </summary>
        public RegistrationController()
            :this(new RegistrationProvider(), new EventProvider())
        {
        }

        /// <summary>
        /// User has registered.
        /// </summary>
        /// <param name="model">The model.</param>
        public void UserRegistered(RegisterModel model)
        {
            registrationProvider.UserRegistered(model);
        }
        
        /// <summary>
        /// USer has been verified.
        /// </summary>
        /// <param name="model">The model.</param>
        public void UserVerified(NotificationModel model)
        {
            EventModel eventModel = new EventModel(model.Guid, Event.UserVerified);
            eventProvider.InsertEvent(eventModel);
        }
    }
}
