namespace Spectrum.Content.Payments.Translators
{
    using Application.Extensions;
    using Braintree;
    using ViewModels;

    public class TransactionTranslator : ITransactionTranslator
    {
        /// <inheritdoc />
        /// <summary>
        /// Translates the specified transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public TransactionViewModel Translate(Transaction transaction)
        {
            return new TransactionViewModel
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                TransactionDateTime = transaction.CreatedAt,
                Status = transaction.Status.ToString().UppercaseFirst(),
                Type = transaction.Type.ToString().UppercaseFirst(),
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
