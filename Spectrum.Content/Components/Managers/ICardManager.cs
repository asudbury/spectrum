namespace Spectrum.Content.Components.Managers
{
    using ViewModels;

    public interface ICardManager
    {
        /// <summary>
        /// Gets the cards.
        /// </summary>
        /// <param name="cardStackName">Name of the card stack.</param>
        /// <returns></returns>
        CardStackViewModel GetCardStack(string cardStackName);
    }
}