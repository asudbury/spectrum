namespace Spectrum.Content.Payments.Translators
{
    using Braintree;
    using Models;
    using System;

    public class PaymentTranslator : IPaymentTranslator
    {
        /// <inheritdoc />
        /// <summary>
        /// Translates the specified transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public PaymentModel Translate(Transaction transaction)
        {
            return new PaymentModel
            {
                Amount = transaction.Amount ?? 0,
                CardType = transaction.CreditCard.CardType.ToString(),
                MaskedCardNumber = transaction.CreditCard.MaskedNumber,
                CreatedTime = DateTime.UtcNow,
                PaymentId = transaction.Id
            };
        }
    }
}
