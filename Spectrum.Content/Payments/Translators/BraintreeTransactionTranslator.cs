namespace Spectrum.Content.Payments.Translators
{
    using Braintree;
    using Interfaces;
    using Scorchio.ExtensionMethods;
    using ViewModels;

    public class BraintreeTransactionTranslator : IBraintreeTransactionTranslator
    {
        /// <inheritdoc />
        /// <summary>
        /// Translates the specified transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public BraintreeTransactionViewModel Translate(Transaction transaction)
        {
            return new BraintreeTransactionViewModel
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                TransactionDateTime = transaction.CreatedAt,
                Status = transaction.Status.ToString().UpperCaseFirstCharacter(),
                Type = transaction.Type.ToString().UpperCaseFirstCharacter(),
                CurrencyType = transaction.CurrencyIsoCode,
                CardType = GetCardType(transaction),
                MaskedNumber = GetMaskedNumber(transaction)
            };
        }

        /// <summary>
        /// Gets the type of the card.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        private string GetCardType(Transaction transaction)
        {
            string cardType = string.Empty;

            if (transaction.CreditCard != null)
            {
                cardType = transaction.CreditCard.CardType.ToString();
            }

            return cardType;
        }

        /// <summary>
        /// Gets the masked number.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        private string GetMaskedNumber(Transaction transaction)
        {
            string maskedNumber = string.Empty;

            if (transaction.CreditCard != null)
            {
                maskedNumber = transaction.CreditCard.MaskedNumber;
            }

            return maskedNumber;
        }
    }
}
