namespace Spectrum.Model.Registration
{
    using System;

    /// <summary>
    /// The RegisterModel class.
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the email address.
        /// </summary>
        public string EmailAddress { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterModel" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="guid">The identifier.</param>
        public RegisterModel(
            string name, 
            string emailAddress, 
            Guid guid)
        {
            Name = name;
            EmailAddress = emailAddress;
            Guid = guid;
        }
    }
}
