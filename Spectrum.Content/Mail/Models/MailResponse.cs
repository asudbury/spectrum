﻿namespace Spectrum.Content.Mail.Models
{
    public class MailResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MailResponse"/> is sent.
        /// </summary>
        public bool Sent { get; set; }

        /// <summary>
        /// Gets or sets to email address.
        /// </summary>
        public string ToEmailAddress { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MailResponse"/> is surpressed.
        /// </summary>
        public bool Surpressed { get; set; }

        /// <summary>
        /// Gets or sets the contents.
        /// </summary>
        public string Contents { get; set; }
    }
}
