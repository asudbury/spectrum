namespace Spectrum.Content.Payments.Translators
{
    using Content.Services;
    using Interfaces;
    using Models;
    using System;
    using ViewModels;

    public class TransactionTranslator : ITransactionTranslator
    {
        /// <summary>
        /// The URL service.
        /// </summary>
        private readonly IUrlService urlService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionTranslator" /> class.
        /// </summary>
        /// <param name="urlService">The URL service.</param>
        public TransactionTranslator(IUrlService urlService)
        {
            this.urlService = urlService;
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
                CreatedTime = model.CreatedTime,
                CreatedUser = model.CreatedUser,
                Amount = "£" + Math.Round(model.Amount,2),
                TransactionDateTime = model.CreatedTime,
                Environment = GetEnvironment(model.Environment),
                PaymentProvider = GetPaymentProvider(model.PaymemtProvider),
                Status = "Paid",
                Type = GetType(model.TransactionType),
                CardType = model.CardType,
                MaskedNumber = model.MaskedCardNumber,
                ViewTransactionUrl = urlService.GetViewPaymenteUrl(model.ClientId, model.TransactionId)
            };
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        internal string GetType(string type)
        {
            return type == "S" ? "Payment" : "Unknown";
        }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <returns></returns>
        internal string GetEnvironment(string environment)
        {
            return environment == "S" ? "Sandbox" : "Production";
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
    }
}
