using Braintree;

namespace Spectrum.Content.Payments.Translators
{
    using Interfaces;
    using Messages;
    using Models;
    using System;

    public class PaymentTranslator : IPaymentTranslator
    {
        /// <summary>
        /// Translates the specified transaction.
        /// </summary>
        /// L<param name="paymentMadeMessage">The payment made message.</param>
        /// <returns></returns>L
        /// <inheritdoc />
        public TransactionModel Translate(TransactionMadeMessage paymentMadeMessage)
        {
            TransactionType transactionType = paymentMadeMessage.Transaction.Type;

            return new TransactionModel
            {
                Amount = paymentMadeMessage.Transaction.Amount ?? 0,
                CardType = paymentMadeMessage.Transaction.CreditCard.CardType.ToString(),
                MaskedCardNumber = paymentMadeMessage.Transaction.CreditCard.MaskedNumber,
                CreatedTime = DateTime.UtcNow,
                TransactionId = paymentMadeMessage.Transaction.Id,
                CreatedUser = paymentMadeMessage.CreatedUser,
                PaymemtProvider = paymentMadeMessage.PaymentProvider.ToUpper().Substring(0),
                Environment = paymentMadeMessage.Environment.ToUpper().Substring(0),
                TransactionType = transactionType.ToString().ToUpper().Substring(0)
            };
        }
    }
}
