namespace Spectrum.Content.Correspondence.Controllers
{
    using Manangers;
    using Services;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using ViewModels;

    public class ContactUsController : BaseController
    {
        /// <summary>
        /// The contact us manager.
        /// </summary>
        private readonly IContactUsManager contactUsManager;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.Correspondence.Controllers.ContactUsController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="contactUsManager">The contact us manager.</param>
        public ContactUsController(
            ILoggingService loggingService,
            IContactUsManager contactUsManager) 
            : base(loggingService)
        {
            this.contactUsManager = contactUsManager;
        }

        /// <summary>
        /// Inserts the contact us.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertContactUs(ContactUsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            IPublishedContent publishedContent = GetContentById(CurrentPage.Id.ToString());
           
            string nextUrl = contactUsManager.InsertContactUs(
                UmbracoContext,
                publishedContent,
                viewModel);

            return Redirect(nextUrl);
        }
    }
}
