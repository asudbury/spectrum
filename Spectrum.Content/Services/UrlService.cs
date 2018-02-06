using System;
using System.Web;

namespace Spectrum.Content.Services
{
    using Application.Services;

    public class UrlService : IUrlService
    {
        /// <summary>
        /// The encrypt parameters.
        /// </summary>
        private bool encryptParameters = true;

        /// <summary>
        /// The encryption service.
        /// </summary>
        private readonly IEncryptionService encryptionService;

        /// <summary>
        /// The rules engine service.
        /// </summary>
        private readonly IRulesEngineService rulesEngineService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlService" /> class.
        /// </summary>
        /// <param name="encryptionService">The encryption service.</param>
        /// <param name="rulesEngineService">The rules engine service.</param>
        public UrlService(
            IEncryptionService encryptionService,
            IRulesEngineService rulesEngineService)
        {
            this.encryptionService = encryptionService;
            this.rulesEngineService = rulesEngineService;
        }

        /// <summary>
        /// Gets the view client URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        public string GetViewClientUrl(int clientId)
        {
            string clientIdParam = GetClientIdParam(clientId);

            return "/customer/clients/view" + clientIdParam;
        }

        /// <summary>
        /// Gets the update client URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        public string GetUpdateClientUrl(int clientId)
        {
            string clientIdParam = GetClientIdParam(clientId);

            return "/customer/clients/update" + clientIdParam;
        }

        /// <summary>
        /// Gets the create quote URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        public string GetCreateQuoteUrl(int clientId)
        {
            if (rulesEngineService.IsCustomerQuotesEnabled())
            {
                string clientIdParam = GetClientIdParam(clientId);

                return "/customer/quotes/create" + clientIdParam;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the create invoice URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        public string GetCreateInvoiceUrl(int clientId)
        {
            if (rulesEngineService.IsCustomerInvoicesEnabled())
            {
                string clientIdParam = GetClientIdParam(clientId);

                return "/customer/invoices/create" + clientIdParam;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the create appointment URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        public string GetCreateAppointmentUrl(int clientId)
        {
            if (rulesEngineService.IsCustomerAppointmentsEnabled())
            {
                string clientIdParam = GetClientIdParam(clientId);

                return "/customer/appointments/create" + clientIdParam;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the make payment URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        public string GetMakePaymentUrl(
            int clientId, 
            int invoiceId,
            string amount)
        {
            if (rulesEngineService.IsCustomerPaymentsEnabled())
            {
                string clientIdParam = GetClientIdParam(clientId);

                return "/customer/payments/pay" + clientIdParam +
                       GetParam(Constants.QueryString.InvoiceId, invoiceId) +
                       GetParam(Constants.QueryString.PaymenyAmount, amount);
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the customer make payment URL.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        public string GetCustomerMakePaymentUrl(
            int customerId,
            int clientId, 
            int invoiceId, 
            string amount)
        {
            string clientIdParam = GetClientIdParam(clientId);

            HttpContextWrapper httpContextWrapper = new HttpContextWrapper(HttpContext.Current);

            string baseUrl = httpContextWrapper.Request.Url.Authority.ToString();

            return GetHttpProtocol() + baseUrl + 
                   "/securepayment" + clientIdParam +
                   GetParam(Constants.QueryString.CustomerId, customerId) +
                   GetParam(Constants.QueryString.InvoiceId, invoiceId) +
                   GetParam(Constants.QueryString.PaymenyAmount, amount);
        }

        /// <summary>
        /// Gets the view paymente URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="paymentId">The payment identifier.</param>
        /// <returns></returns>
        public string GetViewPaymenteUrl(
            int clientId, 
            string paymentId)
        {
            if (rulesEngineService.IsCustomerPaymentsEnabled())
            {
                string clientIdParam = GetClientIdParam(clientId);

                return "/customer/payments/view" + clientIdParam +
                       GetParam(Constants.QueryString.PaymentId, paymentId);
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the view quote URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="quoteId">The quote identifier.</param>
        /// <returns></returns>
        public string GetViewQuoteUrl(
            int clientId, 
            int quoteId)
        {
            if (rulesEngineService.IsCustomerQuotesEnabled())
            { 
                return "/customer/quotes/view" + 
                       GetClientIdParam(clientId) +
                       GetParam(Constants.QueryString.QuoteId, quoteId);
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the update quote URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="quoteId">The quote identifier.</param>
        /// <returns></returns>
        public string GetUpdateQuoteUrl(
            int clientId,
            int quoteId)
        {
            if (rulesEngineService.IsCustomerQuotesEnabled())
            {
                return "/customer/quotes/update" +
                       GetClientIdParam(clientId) +
                       GetParam(Constants.QueryString.QuoteId, quoteId);
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the view invoice URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <returns></returns>
        public string GetViewInvoiceUrl(
            int clientId, 
            int invoiceId)
        {
            if (rulesEngineService.IsCustomerInvoicesEnabled())
            { 
                return "/customer/invoices/view" + 
                       GetClientIdParam(clientId) +
                       GetParam(Constants.QueryString.InvoiceId, invoiceId);
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the update invoice URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <returns></returns>
        public string GetUpdateInvoiceUrl(
            int clientId, 
            int invoiceId)
        {
            if (rulesEngineService.IsCustomerInvoicesEnabled())
            { 
                return "/customer/invoices/update" + 
                       GetClientIdParam(clientId) +
                       GetParam(Constants.QueryString.InvoiceId, invoiceId);
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the email invoice URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <returns></returns>
        public string GetEmailInvoiceUrl(
            int clientId, 
            int invoiceId)
        {
            if (rulesEngineService.IsCustomerInvoicesEnabled())
            {
                return "/customer/invoices/email" +
                       GetClientIdParam(clientId) +
                       GetParam(Constants.QueryString.InvoiceId, invoiceId);
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the view appointment URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <returns></returns>
        public string GetViewAppointmentUrl(
            int clientId, 
            int appointmentId)
        {
            if (rulesEngineService.IsCustomerAppointmentsEnabled())
            {
                return "/customer/appointments/view" +
                       GetClientIdParam(clientId) +
                       GetParam(Constants.QueryString.AppointmentId, appointmentId);
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the update appointment URL.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <returns></returns>
        public string GetUpdateAppointmentUrl(
            int clientId, 
            int appointmentId)
        {
            if (rulesEngineService.IsCustomerAppointmentsEnabled())
            {
                return "/customer/appointments/update" +
                       GetClientIdParam(clientId) +
                       GetParam(Constants.QueryString.AppointmentId, appointmentId);
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the google search URL.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public string GetGoogleSearchUrl(string address)
        {
            if (string.IsNullOrEmpty(address) == false)
            {
                return "http://www.google.co.uk/maps/search/" + address.Replace(" ", "+");
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the encrypted client identifier.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        internal string GetClientIdParam(int clientId)
        {
            return "?" + Constants.QueryString.ClientId + "=" + GetEncryptedString(clientId.ToString());
        }

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        internal string GetParam(
            string name, 
            int id)
        {
            return "&" + name + "="  + GetEncryptedString(id.ToString());
        }

        internal string GetParam(
            string name,
            string param)
        {
            return "&" + name + "=" + GetEncryptedString(param);
        }
        /// <summary>
        /// Gets the encrypted string.
        /// </summary>
        /// <param name="pararm">The pararm.</param>
        /// <returns></returns>
        internal string GetEncryptedString(string pararm)
        {
            return encryptParameters ? encryptionService.EncryptString(pararm) : pararm;
        }

        /// <summary>
        /// Gets the HTTP protocol.
        /// </summary>
        /// <returns></returns>
        internal string GetHttpProtocol()
        {
            return "http://";
        }
    }
}
