namespace Spectrum.Content.Services
{
    using Umbraco.Core.Models;

    public interface ISettingsService
    {
        /// <summary>
        /// Clears the cache.
        /// </summary>
        void ClearCache();

        /// <summary>
        /// Gets the settings node.
        /// </summary>
        /// <returns></returns>
        IPublishedContent GetSettingsNode();

        /// <summary>
        /// Gets the customer node.
        /// </summary>
        /// <returns></returns>
        IPublishedContent GetCustomerNode();

        /// <summary>
        /// Gets the menus node.
        /// </summary>
        /// <returns></returns>
        IPublishedContent GetMenusNode();

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        IPublishedContent GetMenu(string name);

        /// <summary>
        /// Gets the payments node.
        /// </summary>
        /// <returns></returns>
        IPublishedContent GetPaymentsNode();

        /// <summary>
        /// Gets the mail node.
        /// </summary>
        /// <returns></returns>
        IPublishedContent GetMailNode();

        /// <summary>
        /// Gets the mail templates folder node.
        /// </summary>
        /// <returns></returns>
        IPublishedContent GetMailTemplatesFolderNode();

        /// <summary>
        /// Gets the mail template.
        /// </summary>
        /// <param name="templateName">Name of the template.</param>
        /// <returns></returns>
        IPublishedContent GetMailTemplate(string templateName);

        /// <summary>
        /// Gets the mail template.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <returns></returns>
        IPublishedContent GetMailTemplate(
           string folderName,
            string templateName);

        /// <summary>
        /// Gets the mail template by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IPublishedContent GetMailTemplateById(int id);

        /// <summary>
        /// Gets the mail template by URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        IPublishedContent GetMailTemplateByUrl(string url);

        /// <summary>
        /// Gets the appointments node.
        /// </summary>
        /// <returns></returns>
        IPublishedContent GetAppointmentsNode();

        /// <summary>
        /// Gets the quotes node.
        /// </summary>
        /// <returns></returns>
        IPublishedContent GetQuotesNode();

        /// <summary>
        /// Gets the invoices node.
        /// </summary>
        /// <returns></returns>
        IPublishedContent GetInvoicesNode();

        /// <summary>
        /// Gets the cards node.
        /// </summary>
        /// <returns></returns>
        IPublishedContent GetCardsNode();

        /// <summary>
        /// Gets the card stack.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        IPublishedContent GetCardStack(string name);
    }
}
