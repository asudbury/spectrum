namespace Spectrum.Model.Customer
{
    using System;

    /// <summary>
    /// The UpdateNameModel class.
    /// </summary>
    public class UpdateNameModel
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNameModel" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="guid">The identifier.</param>
        public UpdateNameModel(
            string name, 
            Guid guid)
        {
            Name = name;
            Guid = guid;
        }
    }
}
