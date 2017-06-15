namespace Spectrum.Content.Services
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public interface ISettingsService
    {
        /// <summary>
        /// Gets the settings node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IPublishedContent GetSettingsNode(UmbracoContext context);

        /// <summary>
        /// Gets the menus node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IPublishedContent GetMenusNode(UmbracoContext context);

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        IPublishedContent GetMenu(
            UmbracoContext context,
            string name);


        /// <summary>
        /// Gets the payments node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IPublishedContent GetPaymentsNode(UmbracoContext context);

        /// <summary>
        /// Gets the mail node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IPublishedContent GetMailNode(UmbracoContext context);

        /// <summary>
        /// Gets the mail templates folder node.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns></returns>
        IPublishedContent GetMailTemplatesFolderNode(
            UmbracoContext context,
            string folderName);
        
        /// <summary>
        /// Gets the mail template.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <returns></returns>
        IPublishedContent GetMailTemplate(
            UmbracoContext context, 
            string folderName,
            string templateName);

    }
}
