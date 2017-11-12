namespace Spectrum.Content.Payments.Providers
{
    using Models;

    /// <summary>
    /// 
    /// </summary>
    public interface IDatabaseProvider
    {
        /// <summary>
        /// Inserts the payment.
        /// </summary>
        /// <param name="model">The model.</param>
        void InsertPayment(PaymentModel model);
    }
}