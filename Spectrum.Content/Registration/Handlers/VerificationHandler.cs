namespace Spectrum.Content.Registration.Handlers
{
    using Application.Registration.Controllers;
    using Models;
    
    /// <summary>
    /// 
    /// </summary>
    public class VerificationHandler
    {
        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(UserVerificationCompleteMessage message)
        {
            RegistrationController controller = new RegistrationController();
            controller.UserVerified(message.Content);
        }
    }
}
