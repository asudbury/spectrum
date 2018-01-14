namespace Spectrum.Content.Services
{
    public interface IUrlService
    {
        /// <summary>
        /// Gets the view client URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        string GetViewClientUrl(int clientId);

        /// <summary>
        /// Gets the update client URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        string GetUpdateClientUrl(int clientId);
        
        /// <summary>
        /// Gets the create quote URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        string GetCreateQuoteUrl(int clientId);

        /// <summary>
        /// Gets the create invoice URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        string GetCreateInvoiceUrl(int clientId);

        /// <summary>
        /// Gets the create appointment URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        string GetCreateAppointmentUrl(int clientId);

        /// <summary>
        /// Gets the make paymente URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        string GetMakePaymenteUrl(
            int clientId,
            int invoiceId,
            string amount);

        /// <summary>
        /// Gets the view paymente URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="paymentId">The payment identifier.</param>
        /// <returns></returns>
        string GetViewPaymenteUrl(
            int clientId,
            string paymentId);

        /// <summary>
        /// Gets the view quote URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="quoteId">The quote identifier.</param>
        /// <returns></returns>
        string GetViewQuoteUrl(
            int clientId,
            int quoteId);

        /// <summary>
        /// Gets the update quote URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="quoteId">The quote identifier.</param>
        /// <returns></returns>
        string GetUpdateQuoteUrl(
            int clientId,
            int quoteId);

        /// <summary>
        /// Gets the view invoice URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <returns></returns>
        string GetViewInvoiceUrl(
            int clientId,
            int invoiceId);

        /// <summary>
        /// Gets the update invoice URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <returns></returns>
        string GetUpdateInvoiceUrl(
            int clientId,
            int invoiceId);

        string GetViewAppointmentUrl(
            int clientId,
            int appointmentId);

        string GetUpdateAppointmentUrl(
            int clientId,
            int appointmentId);

        /// <summary>
        /// Gets the google search URL.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        string GetGoogleSearchUrl(string address);
    }
}