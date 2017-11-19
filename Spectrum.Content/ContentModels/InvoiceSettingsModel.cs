namespace Spectrum.Content.ContentModels
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class InvoiceSettingsModel : BaseModel
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Umbraco.Core.Models.PublishedContent.PublishedContentModel" /> class with
        /// an original <see cref="T:Umbraco.Core.Models.IPublishedContent" /> instance.
        /// </summary>
        /// <param name="content">The original content.</param>
        public InvoiceSettingsModel(IPublishedContent content) : base(content)
        {
        }

        /// <summary>
        /// Gets a value indicating whether [invoices enabled].
        /// </summary>
        public bool InvoicesEnabled => this.GetPropertyValue<bool>("invoicesEnabled");

        /// <summary>
        /// Gets the invoice email template.
        /// </summary>
        public string InvoiceEmailTemplate => this.GetPropertyValue<string>("invoiceEmailTemplate");
    }
}
