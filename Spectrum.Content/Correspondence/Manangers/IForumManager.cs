namespace Spectrum.Content.Correspondence.Manangers
{
    using ViewModels;

    public interface IForumManager
    {
        /// <summary>
        /// Posts the message.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        void PostMessage(ForumPostViewModel viewModel);
    }
}