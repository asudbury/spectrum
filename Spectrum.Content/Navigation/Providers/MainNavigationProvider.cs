namespace Spectrum.Content.Navigation.Providers
{
    using ContentModels;
    using System.Collections.Generic;
    using Umbraco.Core.Models;

    public class MainNavigationProvider : IMainNavigationProvider
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public IEnumerable<MenuItemModel> GetMenu(IPublishedContent content)
        {
            List<MenuItemModel> models = new List<MenuItemModel>();

            foreach (IPublishedContent contentChild in content.Children)
            {
                MenuItemModel model = new MenuItemModel(contentChild);

                models.Add(model);
            }
            
            return models;
        }
    }
}
