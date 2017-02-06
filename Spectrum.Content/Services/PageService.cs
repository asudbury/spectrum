namespace Spectrum.Content.Services
{
    using System.Collections.Specialized;
    using System.Configuration;

    public class PageService : IPageService
    {
        /// <summary>
        /// The settings.
        /// </summary>
        private static readonly NameValueCollection Settings = ConfigurationManager.GetSection("SpectrumPageGuids") as NameValueCollection;

        /// <summary>
        /// Gets the registration complete page.
        /// </summary>
        public string RegistrationCompletePage
        {
            get { return string.Empty; }
        }
    }
}
