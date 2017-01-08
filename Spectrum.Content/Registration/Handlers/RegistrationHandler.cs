namespace Spectrum.Content.Registration.Handlers
{
    using Application.Registration.Controllers;
    using Models;

    public class RegistrationHandler
    {
        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(RegistrationCompleteMessage message)
        {
            RegistrationController controller = new RegistrationController();
            controller.UserRegistered(message.Content);
        }
    }
}
