namespace Spectrum.Model
{
    using System;

    /// <summary>
    /// The Notification Model
    /// </summary>
    public class NotificationModel
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationModel" /> class.
        /// </summary>
        /// <param name="guid">The identifier.</param>
        public NotificationModel(Guid guid)
        {
            Guid = guid;
        }
    }
}
