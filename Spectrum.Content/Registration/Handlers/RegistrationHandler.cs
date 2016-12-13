namespace Spectrum.Content.Registration.Handlers
{
    using Application.Registration.Controllers;
    using Messages;

    public class RegistrationHandler
    {
        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(RegistrationMessage message)
        {
            RegistrationController controller = new RegistrationController();
            controller.RegisterUser(message.Content);
        }
    }
}
