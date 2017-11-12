namespace Spectrum.Content.Payments.Translators
{
    using Messages;
    using Models;
    using System;

    public class PaymentTranslator : IPaymentTranslator
    {
        /// <summary>
        /// Translates the specified transaction.
        /// </summary>
        /// <param name="paymentMadeMessage">The payment made message.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public PaymentModel Translate(PaymentMadeMessage paymentMadeMessage)
        {
            return new PaymentModel
            {
                Amount = paymentMadeMessage.Transaction.Amount ?? 0,
                CardType = paymentMadeMessage.Transaction.CreditCard.CardType.ToString(),
                MaskedCardNumber = paymentMadeMessage.Transaction.CreditCard.MaskedNumber,
                CreatedTime = DateTime.UtcNow,
                PaymentId = paymentMadeMessage.Transaction.Id,
                CreatedUser = paymentMadeMessage.CreatedUser
            };
        }
    }
}
