namespace Spectrum.Content.ContentModels
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class CustomerModel : BaseModel
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Umbraco.Core.Models.PublishedContent.PublishedContentModel" /> class with
        /// an original <see cref="T:Umbraco.Core.Models.IPublishedContent" /> instance.
        /// </summary>
        /// <param name="content">The original content.</param>
        public CustomerModel(IPublishedContent content) : base(content)
        {
        }

        /// <summary>
        /// Gets the customer enabled.
        /// </summary>
        public bool CustomerEnabled => this.GetPropertyValue<bool>("customerEnabled");

        /// <summary>
        /// Gets the home page URL.
        /// </summary>
        public string HomePageUrl => this.GetPropertyValue<string>("homepageUrl");

        /// <summary>
        /// Gets the name of the customer.
        /// </summary>
        public string CustomerName => this.GetPropertyValue<string>("customerName");

        /// <summary>
        /// Gets the address.
        /// </summary>
        public string Address => this.GetPropertyValue<string>("address");

        /// <summary>
        /// Gets the location.
        /// </summary>
        public string Location => this.GetPropertyValue<string>("location");

        /// <summary>
        /// Gets the email address.
        /// </summary>
        public string EmailAddress => this.GetPropertyValue<string>("emailAddress");

        /// <summary>
        /// Gets the daytime phone number.
        /// </summary>
        public string DaytimePhoneNumber => this.GetPropertyValue<string>("dayTimePhone");

        /// <summary>
        /// Gets the evening phone number.
        /// </summary>
        public string EveningPhoneNumber => this.GetPropertyValue<string>("dayTimePhone");

        /// <summary>
        /// Gets the mobile phone number.
        /// </summary>
        public string MobilePhoneNumber => this.GetPropertyValue<string>("mobilePhone");

        /// <summary>
        /// Gets the users.
        /// </summary>
        public string Users => this.GetPropertyValue<string>("users");
    }
}
