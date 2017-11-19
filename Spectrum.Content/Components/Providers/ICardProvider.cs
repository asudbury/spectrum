namespace Spectrum.Content.Components.Providers
{
    using Models;
    using System.Collections.Generic;
    using Umbraco.Core.Models;

    public interface ICardProvider
    {
        /// <summary>
        /// Gets the cards.
        /// </summary>
        /// <param name="cardStackNode">The card stack node.</param>
        /// <returns></returns>
        IEnumerable<CardModel> GetCards(IPublishedContent cardStackNode);
    }
}