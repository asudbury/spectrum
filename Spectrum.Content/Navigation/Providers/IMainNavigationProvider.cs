namespace Spectrum.Content.Navigation.Providers
{
    using ContentModels;
    using System.Collections.Generic;
    using Umbraco.Core.Models;

    public interface IMainNavigationProvider
    {
        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        IEnumerable<MenuItemModel> GetMenu(IPublishedContent content);
    }
}
