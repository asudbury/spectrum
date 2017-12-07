namespace Spectrum.Content.Correspondence.Controllers
{
    using Manangers;
    using Services;
    using ViewModels;

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
        /// Posts the message.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public void PostMessage(ForumPostViewModel viewModel)
        {
            forumManager.PostMessage(viewModel);
        }
    }
}
