namespace Spectrum.Content.Registration.Models
{
    using Model.Correspondence;
    using TinyMessenger;

    /// <summary>
    /// The RegistrationCompleteMessage class.
    /// </summary>
    /// <seealso cref="TinyMessenger.GenericTinyMessage{NotificationModel}" />
    public class UserVerificationCompleteMessage : GenericTinyMessage<NotificationModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserVerificationCompleteMessage"/> class.
        /// </summary>
        /// <param name="sender">Message sender (usually "this")</param>
        /// <param name="content">Contents of the message</param>
        public UserVerificationCompleteMessage(object sender, NotificationModel content)
            : base(sender, content)
        {
        }
    }
}
