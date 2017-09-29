namespace Spectrum.Content.Correspondence.Controllers
{
    using Manangers;
    using Services;
    using ViewModels;
    using Umbraco.Web;

    public class ForumController : BaseController
    {
        /// <summary>
        /// The forum manager.
        /// </summary>
        private readonly IForumManager forumManager;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="forumManager"></param>
        public ForumController(
            ILoggingService loggingService,
            IForumManager forumManager) 
            : base(loggingService)
        {
            this.forumManager = forumManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="forumManager">The forum manager.</param>
        /// <inheritdoc />
        public ForumController(
            ILoggingService loggingService, 
            UmbracoContext umbracoContext,
            IForumManager forumManager) 
            : base(loggingService, umbracoContext)
        {
            this.forumManager = forumManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="umbracoHelper">The umbraco helper.</param>
        /// <param name="forumManager">The forum manager.</param>
        /// <inheritdoc />
        public ForumController(
            ILoggingService loggingService, 
            UmbracoContext umbracoContext, 
            UmbracoHelper umbracoHelper,
            IForumManager forumManager) 
            : base(loggingService, umbracoContext, umbracoHelper)
        {
            this.forumManager = forumManager;
        }

        /// <summary>
        /// Posts the message.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public void PostMessage(ForumPostViewModel viewModel)
        {
            forumManager.PostMessage(viewModel);
        }
    }
}
