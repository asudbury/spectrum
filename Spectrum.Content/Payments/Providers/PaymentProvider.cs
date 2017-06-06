﻿namespace Spectrum.Content.Payments.Providers
{
    using ContentModels;
    using Services;
    using ViewModels;

    public class PaymentProvider : IPaymentProvider
    {
        /// <summary>
        /// The braintree service.
        /// </summary>
        private readonly IBraintreeService braintreeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentProvider"/> class.
        /// </summary>
        /// <param name="braintreeService">The braintree service.</param>
        public PaymentProvider(IBraintreeService braintreeService)
        {
            this.braintreeService = braintreeService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentProvider"/> class.
        /// </summary>
        public PaymentProvider()
            : this(new BraintreeService())
        {
        }

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public string GetAuthToken(BraintreeModel model)
        {
            return braintreeService.GetAuthToken(model);
        }

        /// <summary>
        /// Makes the payment.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public bool MakePayment(
            BraintreeModel model,
            PaymentViewModel viewModel)
        {
            return braintreeService.MakePayment(model, viewModel);
        }
    }
}