namespace Spectrum.Content.Registration.Model
{
    using System;
    using Umbraco.Core.Models;

    /// <summary>
    /// The RegisteredUser class.
    /// </summary>
    public class RegisteredUser
    {
        /// <summary>
        /// Gets the member.
        /// </summary>
        public IMember Member { get; }

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisteredUser"/> class.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="guid">The unique identifier.</param>
        public RegisteredUser(
            IMember member, 
            Guid guid)
        {
            Member = member;
            Guid = guid;
        }
    }
}
