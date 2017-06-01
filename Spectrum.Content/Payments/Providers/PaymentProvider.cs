namespace Spectrum.Content.Payments.Providers
{
    using Services;

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
        /// Makes the payment.
        /// </summary>
        /// <returns></returns>
        public bool MakePayment()
        {
            ////braintreeService.GetGateway();

            ////braintreeService.GetToken();

            ////braintreeService.MakePayment();

            return true;
        }
    }
}
