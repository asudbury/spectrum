namespace Spectrum.Content
{
    using Model;
    using TinyMessenger;

    /// <summary>
    /// The NotificationMessage class.
    /// </summary>
    public class NotificationMessage : GenericTinyMessage<NotificationModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationMessage"/> class.
        /// </summary>
        /// <param name="sender">Message sender (usually "this")</param>
        /// <param name="content">Contents of the message</param>
        public NotificationMessage(object sender, NotificationModel content)
            : base(sender, content)
        {
        }
    }
}
