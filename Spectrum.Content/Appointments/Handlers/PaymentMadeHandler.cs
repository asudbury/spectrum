namespace Spectrum.Content.Appointments.Handlers
{
    using Autofac.Events;
    using Messages;
    using Managers;

    public class PaymentMadeHandler : IHandleEvent<PaymentMadeMessage>
    {
        /// <summary>
        /// The payment made manager.
        /// </summary>
        private readonly IPaymentMadeManager paymentMadeManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentMadeHandler"/> class.
        /// </summary>
        /// <param name="paymentMadeManager">The payment made manager.</param>
        public PaymentMadeHandler(IPaymentMadeManager paymentMadeManager)
        {
            this.paymentMadeManager = paymentMadeManager;
        }

        /// <summary>
        /// Handles the specified payment made message.
        /// </summary>
        /// <param name="paymentMadeMessage">The payment made message.</param>
        public void Handle(PaymentMadeMessage paymentMadeMessage)
        {
            paymentMadeManager.Handle(paymentMadeMessage);
        }
    }
}
