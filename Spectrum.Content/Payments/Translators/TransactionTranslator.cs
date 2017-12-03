namespace Spectrum.Content.Payments.Translators
{
    using Application.Services;
    using Interfaces;
    using Models;
    using System;
    using ViewModels;

    public class TransactionTranslator : ITransactionTranslator
    {
        /// <summary>
        /// The encryption service.
        /// </summary>
        private readonly IEncryptionService encryptionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionTranslator"/> class.
        /// </summary>
        /// <param name="encryptionService">The encryption service.</param>
        public TransactionTranslator(IEncryptionService encryptionService)
        {
            this.encryptionService = encryptionService;
        }

        /// <inheritdoc />
        /// <summary>
        /// Translates the specified transaction.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionViewModel Translate(TransactionModel model)
        {
            return new TransactionViewModel
            {
                Id = model.TransactionId,
                CreatedUser = model.CreatedUser,
                Amount = "£" + Math.Round(model.Amount,2),
                TransactionDateTime = model.CreatedTime,
                Environment = GetEnvironment(model.Environment),
                PaymentProvider = GetPaymentProvider(model.PaymemtProvider),
                Status = "Paid",
                Type = GetType(model.TransactionType),
                CardType = model.CardType,
                MaskedNumber = model.MaskedCardNumber,
                ViewTransactionUrl = BuildUrl(model.Id)
            };
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        internal string GetType(string type)
        {
            if (type == "S")
            {
                return "Payment";
            }

            return "Unknown";
        }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <returns></returns>
        internal string GetEnvironment(string environment)
        {
            if (environment == "S")
            {
                return "Sandbox";
            }

            return "Production";
        }

        internal string GetPaymentProvider(string paymentProvider)
        {
            switch (paymentProvider)
            {
                case "B":
                    return "Braintree";
                case "P":
                    return "PayPal";
            }

            return paymentProvider;
        }

        /// <summary>
        /// Builds the appointment URL.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns></returns>
        internal string BuildUrl(int transactionId)
        {
            return "viewpayment" + "?" + PaymentsQueryStringConstants.PaymentId + "=" + encryptionService.EncryptString(transactionId.ToString());
        }
    }
}
