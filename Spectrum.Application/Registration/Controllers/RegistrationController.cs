namespace Spectrum.Application.Registration.Controllers
{
    using Model;
    using Model.Registration;
    using Providers;

    public class RegistrationController
    {
        /// <summary>
        /// The registration provider.
        /// </summary>
        private readonly IRegistrationProvider registrationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController" /> class.
        /// </summary>
        /// <param name="registrationProvider">The registration provider.</param>
        public RegistrationController(IRegistrationProvider registrationProvider)
        {
            this.registrationProvider = registrationProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController"/> class.
        /// </summary>
        public RegistrationController()
            :this(new RegistrationProvider())
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
            registrationProvider.UserVerified(model);
        }
    }
}
