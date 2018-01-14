namespace Spectrum.Content.Models
{
    using System;

    public class BaseModel
    {
        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// Gets or sets the last updated time.
        /// </summary>

        public DateTime LastUpdatedTime { get; set; }

        /// <summary>
        /// Gets or sets the last updated user.
        /// </summary>
        public string LastUpdatedUser { get; set; }
    }
}
