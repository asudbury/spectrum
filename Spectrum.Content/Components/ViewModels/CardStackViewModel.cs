namespace Spectrum.Content.Components.ViewModels
{
    using Models;
    using System.Collections.Generic;

    public class CardStackViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardStackViewModel"/> class.
        /// </summary>
        public CardStackViewModel()
        {
            Cards = new List<CardModel>();
        }

        /// <summary>
        /// Gets or sets the name of the card stack.
        /// </summary>
        public string CardStackName { get; set; }

        /// <summary>
        /// Gets or sets the cards.
        /// </summary>
        public IEnumerable<CardModel> Cards { get; set; }
    }
}
