namespace Spectrum.Content.Appointments.Managers
{
    using Messages;

    public interface IPaymentMadeManager
    {
        void Handle(PaymentMadeMessage paymentMadeMessage);
    }
}
