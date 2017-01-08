namespace Spectrum.Model.Correspondence
{
    using System;

    /// <summary>
    /// The EmailSentModel class.
    /// </summary>
    public class EmailSentModel
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
        /// Initializes a new instance of the <see cref="EmailSentModel" /> class.
        /// </summary>
        /// <param name="reference">The email reference.</param>
        /// <param name="guid">The identifier.</param>
        public EmailSentModel(
            string reference, 
            Guid guid)
        {
            Reference = reference;
            Guid = guid;
        }
    }
}
