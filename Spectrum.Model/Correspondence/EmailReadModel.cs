namespace Spectrum.Model.Correspondence
{
    using System;

    /// <summary>
    /// The EmailReadModel class.
    /// </summary>
    public class EmailReadModel
    {
        /// <summary>
        /// Gets the email reference.
        /// </summary>
        public string Reference { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailReadModel" /> class.
        /// </summary>
        /// <param name="reference">The email reference.</param>
        /// <param name="guid">The identifier.</param>
        public EmailReadModel(
            string reference, 
            Guid guid)
        {
            Reference = reference;
            Guid = guid;
        }
    }
}
