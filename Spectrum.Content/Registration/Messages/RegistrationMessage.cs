namespace Spectrum.Content.Registration.Messages
{
    using Application.Registration.Models;
    using TinyMessenger;

    /// <summary>
    /// The RegistrationMessage class.
    /// </summary>
    /// <seealso cref="TinyMessenger.GenericTinyMessage{RegisterModel}" />
    public class RegistrationMessage : GenericTinyMessage<RegisterModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationMessage"/> class.
        /// </summary>
        /// <param name="sender">Message sender (usually "this")</param>
        /// <param name="content">Contents of the message</param>
        public RegistrationMessage(object sender, RegisterModel content)
            : base(sender, content)
        {
        }
    }
}
