namespace Spectrum.Content.Payments.Translators
{
    using Braintree;
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

            DateTime now = DateTime.UtcNow;

            return new TransactionModel
            {
                ClientId = paymentMadeMessage.ClientViewModel.Id,
                Amount = paymentMadeMessage.Transaction.Amount ?? 0,
                CardType = paymentMadeMessage.Transaction.CreditCard.CardType.ToString(),
                MaskedCardNumber = paymentMadeMessage.Transaction.CreditCard.MaskedNumber,
                TransactionId = paymentMadeMessage.Transaction.Id,
                CreatedTime = now,
                CreatedUser = paymentMadeMessage.CreatedUser,
                LastUpdatedTime = now,
                LastUpdatedUser = paymentMadeMessage.CreatedUser,
                PaymemtProvider = paymentMadeMessage.PaymentProvider.ToUpper().Substring(0),
                Environment = paymentMadeMessage.Environment.ToUpper().Substring(0),
                TransactionType = transactionType.ToString().ToUpper().Substring(0)
            };
        }
    }
}
