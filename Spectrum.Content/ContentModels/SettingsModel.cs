﻿namespace Spectrum.Content.ContentModels
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class SettingsModel : BaseModel
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Umbraco.Core.Models.PublishedContent.PublishedContentModel" /> class with
        /// an original <see cref="T:Umbraco.Core.Models.IPublishedContent" /> instance.
        /// </summary>
        /// <param name="content">The original content.</param>
        public SettingsModel(IPublishedContent content) : base(content)
        {
        }

        /// <summary>
        /// Gets a value indicating whether [site enabled].
        /// </summary>
        public bool SiteEnabled => this.GetPropertyValue<bool>("siteEnabled");

        /// <summary>
        /// Gets the offline URL.
        /// </summary>
        public string OfflineUrl => GetNiceUrl(this.GetPropertyValue<int>("offlineUrl"));

        /// <summary>
        /// Gets the page not found node identifier.
        /// </summary>
        public int PageNotFoundNodeId => this.GetPropertyValue<int>("pageNotFoundUrl");

        /// <summary>
        /// Gets the page not found URL.
        /// </summary>
        public string PageNotFoundUrl => GetNiceUrl(this.GetPropertyValue<int>("pageNotFoundUrl"));

        /// <summary>
        /// Gets the email address.
        /// </summary>
        public string EmailAddress => this.GetPropertyValue<string>("emailAddress");

        /// <summary>
        /// Gets the contact us email address.
        /// </summary>
        public string ContactUsEmailAddress => this.GetPropertyValue<string>("contactUsEmailAddress");

    }
}
