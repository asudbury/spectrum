namespace Spectrum.Content.Components.Controllers
{
    using Managers;
    using Services;
    using System.Web.Mvc;
    using ViewModels;

    public class CardController : BaseController
    {
        /// <summary>
        /// The card manager.
        /// </summary>
        private readonly ICardManager cardManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spectrum.Content.BaseController" /> class.
        /// </summary>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="cardManager">The card manager.</param>
        /// <inheritdoc />
        public CardController(
            ILoggingService loggingService,
            ICardManager cardManager) 
            : base(loggingService)
        {
            this.cardManager = cardManager;
        }

        /// <summary>
        /// Gets the cards.
        /// </summary>
        /// <param name="cardStackName">Name of the card stack.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult CardStack(string cardStackName)
        {
            CardStackViewModel viewModel = cardManager.GetCardStack(cardStackName);
            
            return PartialView("Partials/Spectrum/Components/CardStack", viewModel);
        }
    }
}
