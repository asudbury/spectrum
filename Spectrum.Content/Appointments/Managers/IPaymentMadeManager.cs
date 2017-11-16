namespace Spectrum.Content.Appointments.Managers
{
    using Messages;

    /// <summary>
    /// 
    /// </summary>
    public interface IPaymentMadeManager
    {
        /// <summary>
        /// Handles the specified payment made message.
        /// </summary>
        /// <param name="paymentMadeMessage">The payment made message.</param>
        void Handle(TransactionMadeMessage paymentMadeMessage);
    }
}
