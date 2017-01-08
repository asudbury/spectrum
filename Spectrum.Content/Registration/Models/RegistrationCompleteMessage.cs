namespace Spectrum.Content.Registration.Models
{
    using Model.Registration;
    using TinyMessenger;

    /// <summary>
    /// The RegistrationCompleteMessage class.
    /// </summary>
    /// <seealso cref="TinyMessenger.GenericTinyMessage{RegisterModel}" />
    public class RegistrationCompleteMessage : GenericTinyMessage<RegisterModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationCompleteMessage"/> class.
        /// </summary>
        /// <param name="sender">Message sender (usually "this")</param>
        /// <param name="content">Contents of the message</param>
        public RegistrationCompleteMessage(object sender, RegisterModel content)
            : base(sender, content)
        {
        }
    }
}
