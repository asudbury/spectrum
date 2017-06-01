namespace Spectrum.Content.Payments.Providers
{
    public interface IPaymentProvider
    {
        /// <summary>
        /// Makes the payment.
        /// </summary>
        /// <returns></returns>
        bool MakePayment();
    }
}
