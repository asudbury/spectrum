namespace Spectrum.Content.Components.Managers
{
    using Models;
    using Providers;
    using Services;
    using System.Collections.Generic;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public class CardManager : ICardManager
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The card provider.
        /// </summary>
        private readonly ICardProvider cardProvider;

        /// <summary>
        /// The rules engine service
        /// </summary>
        private readonly IRulesEngineService rulesEngineService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardManager" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="cardProvider">The card provider.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        public CardManager(
            ISettingsService settingsService,
            ICardProvider cardProvider,
            IRulesEngineService rulesEngineService)
        {
            this.settingsService = settingsService;
            this.cardProvider = cardProvider;
            this.rulesEngineService = rulesEngineService;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the cards.
        /// </summary>
        /// <returns></returns>
        public CardStackViewModel GetCardStack(string cardStackName)
        {
            IPublishedContent cardStackNode = settingsService.GetCardStack(cardStackName);

            if (cardStackNode != null)
            {
                IEnumerable<CardModel> cardModels = cardProvider.GetCards(cardStackNode);

                List<CardModel> allowedCards = new List<CardModel>();

                foreach (CardModel cardModel in cardModels)
                {
                    if (string.IsNullOrEmpty(cardModel.DisplayRule) == false)
                    {
                        bool result = rulesEngineService.Execute(cardModel.DisplayRule);

                        if (result)
                        {
                            allowedCards.Add(cardModel);
                        }
                    }

                    else
                    {
                        allowedCards.Add(cardModel);
                    }
                }

                CardStackViewModel viewModel = new CardStackViewModel
                {
                    Cards = allowedCards
                };

                return viewModel;
            }

            return new CardStackViewModel();
        }
    }
}
