namespace Spectrum.Content.Components.Providers
{
    using Models;
    using System.Collections.Generic;
    using Umbraco.Core.Models;

    public class CardProvider : ICardProvider
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets the cards.
        /// </summary>
        /// <param name="cardStackNode">The card stack node.</param>
        /// <returns></returns>
        public IEnumerable<CardModel> GetCards(IPublishedContent cardStackNode)
        {
            List<CardModel> models = new List<CardModel>();

            foreach (IPublishedContent contentChild in cardStackNode.Children)
            {
                CardModel model = new CardModel(contentChild);

                models.Add(model);
            }

            return models;
        }
    }
}
