namespace Spectrum.Content.Payments.Providers
{
    using Models;
    using Umbraco.Core;

    public class DatabaseProvider : IDatabaseProvider
    {
        /// <inheritdoc />
        /// <summary>
        /// Inserts the payment.
        /// </summary>
        /// <param name="model">The model.</param>
        public void InsertPayment(PaymentModel model)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            context.Database.Insert(model);
        }
    }
}