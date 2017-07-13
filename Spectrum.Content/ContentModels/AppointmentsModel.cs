namespace Spectrum.Content.ContentModels
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class AppointmentsModel : BaseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Umbraco.Core.Models.PublishedContent.PublishedContentModel" /> class with
        /// an original <see cref="T:Umbraco.Core.Models.IPublishedContent" /> instance.
        /// </summary>
        /// <param name="content">The original content.</param>
        public AppointmentsModel(IPublishedContent content) : base(content)
        {
        }

        /// <summary>
        /// Gets the google calendar integration.
        /// </summary>
        public bool GoogleCalendarIntegration => this.GetPropertyValue<bool>("googleCalendar");

        /// <summary>
        /// Gets a value indicating whether [i cal integration].
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public bool iCalIntegration => this.GetPropertyValue<bool>("iCal");

        /// <summary>
        /// Gets a value indicating whether [database integration].
        /// </summary>
        public bool DatabaseIntegration => this.GetPropertyValue<bool>("database");

        /// <summary>
        /// Gets a value indicating whether [automatic allocate payments].
        /// </summary>
        public bool AutoAllocatePayments => this.GetPropertyValue<bool>("autoAllocatePayments");
        
        /// <summary>
        /// Gets the google calendar URL.
        /// </summary>
        public string GoogleCalendarUrl => this.GetPropertyValue<string>("googleCalendarUrl");

        /// <summary>
        /// Gets the name of the google calendar.
        /// </summary>
        public string GoogleCalendarName => this.GetPropertyValue<string>("googleCalendarName");
        
        /// <summary>
        /// Gets the google client identifier.
        /// </summary>
        public string GoogleClientId => this.GetPropertyValue<string>("googleClientId");

        /// <summary>
        /// Gets the google client secret.
        /// </summary>
        public string GoogleClientSecret => this.GetPropertyValue<string>("googleClientSecret");

        /// <summary>
        /// Gets the redirect URL.
        /// </summary>
        public string RedirectUrl => this.GetPropertyValue<string>("RedirectUrl");
    }
}
