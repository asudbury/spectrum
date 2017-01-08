namespace Spectrum.Model.Customer
{
    using System;

    /// <summary>
    /// The UpdateEmailAddressModel class.
    /// </summary>
    public class UpdateEmailAddressModel
    {
        /// <summary>
        /// Gets the email address.
        /// </summary>
        public string EmailAddress { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEmailAddressModel" /> class.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="guid">The identifier.</param>
        public UpdateEmailAddressModel(
            string emailAddress, 
            Guid guid)
        {
            EmailAddress = emailAddress;
            Guid = guid;
        }
    }
}
