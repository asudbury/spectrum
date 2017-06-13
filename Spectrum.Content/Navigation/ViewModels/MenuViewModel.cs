namespace Spectrum.Content.Navigation.ViewModels
{
    using ContentModels;
    using System.Collections.Generic;

    public class MenuViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuViewModel"/> class.
        /// </summary>
        public MenuViewModel()
        {
            MenuItems = new List<MenuItemModel>();
        }

        /// <summary>
        /// Gets or sets the menu items.
        /// </summary>
        public IEnumerable<MenuItemModel> MenuItems { get; set; }
    }
}
