namespace Spectrum.Content.Services
{
    using ContentModels;
    using Umbraco.Core.Models;

    public class RulesEngineService : IRulesEngineService
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RulesEngineService"/> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public RulesEngineService(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        /// <inheritdoc />
        /// <summary>
        /// Determines whether [is customer appointments enabled].
        /// </summary>
        public bool IsCustomerAppointmentsEnabled()
        {
            AppointmentSettingsModel model = GetAppointmentSettingsModel();

            return model != null && model.AppointmentsEnabled;
        }

        /// <inheritdoc />
        /// <summary>
        /// Determines whether [is customer payments enabled].
        /// </summary>
        public bool IsCustomerPaymentsEnabled()
        {
            PaymentSettingsModel model = GetPaymentsSettingsModel();

            return model != null && model.PaymentsEnabled;
        }

        /// <inheritdoc />
        /// <summary>
        /// Determines whether [is customer dashboard enabled].
        /// </summary>
        public bool IsCustomerDashboardEnabled()
        {
            return IsCustomerQuotesEnabled() ||
                   IsCustomerInvoicesEnabled() ||
                   IsCustomerAppointmentsEnabled() ||
                   IsCustomerPaymentsEnabled();
        }

        /// <inheritdoc />
        /// <summary>
        /// Determines whether [is customer google calendar enabled].
        /// </summary>
        public bool IsCustomerGoogleCalendarEnabled()
        {
            AppointmentSettingsModel model = GetAppointmentSettingsModel();

            return model != null && model.GoogleCalendarEnabled;
        }

        /// <summary>
        /// Determines whether [is customer quotes enabled] [the specified umbraco context].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is customer quotes enabled]; otherwise, <c>false</c>.
        /// </returns>
        /// <inheritdoc />
        public bool IsCustomerQuotesEnabled()
        {
            QuoteSettingsModel model = GetQuoteSettingsModel();

            return model != null && model.QuotesEnabled;
        }

        /// <summary>
        /// Determines whether [is customer invoices enabled] [the specified umbraco context].
        /// </summary>
        /// <returns></returns>
        /// <inheritdoc />
        public bool IsCustomerInvoicesEnabled()
        {
            InvoiceSettingsModel model = GetInvoiceSettingsModel();

            return model != null && model.InvoicesEnabled;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public bool Execute(string query)
        {
            switch (query)
            {
                case Constants.Rules.IsCustomerQuotesEnabled:
                    return IsCustomerQuotesEnabled();

                case Constants.Rules.IsCustomerInvoicesEnabled:
                    return IsCustomerInvoicesEnabled();

                case Constants.Rules.IsCustomerAppointmentsEnabled:
                    return IsCustomerAppointmentsEnabled();

                case Constants.Rules.IsCustomerPaymentsEnabled:
                    return IsCustomerPaymentsEnabled();

                case Constants.Rules.IsCustomerDashboardEnabled:
                    return IsCustomerDashboardEnabled();

                case Constants.Rules.IsCustomerGoogleCalendarEnabled:
                    return IsCustomerGoogleCalendarEnabled();

                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the quote settings model.
        /// </summary>
        /// <returns></returns>
        internal QuoteSettingsModel GetQuoteSettingsModel()
        {
            IPublishedContent quoteNode = settingsService.GetQuotesNode();

            return quoteNode != null ? new QuoteSettingsModel(quoteNode) : null;
        }

        /// <summary>
        /// Gets the invoice settings model.
        /// </summary>
        /// <returns></returns>
        internal InvoiceSettingsModel GetInvoiceSettingsModel()
        {
            IPublishedContent invoiceNode = settingsService.GetInvoicesNode();

            return invoiceNode != null ? new InvoiceSettingsModel(invoiceNode) : null;
        }

        /// <summary>
        /// Gets the appointment settings model.
        /// </summary>
        /// <returns></returns>
        internal AppointmentSettingsModel GetAppointmentSettingsModel()
        {
            IPublishedContent appointmentsNode = settingsService.GetAppointmentsNode();

            return appointmentsNode != null ? new AppointmentSettingsModel(appointmentsNode) : null;
        }

        /// <summary>
        /// Gets the payments settings model.
        /// </summary>
        /// <returns></returns>
        internal PaymentSettingsModel GetPaymentsSettingsModel()
        {
            IPublishedContent paymentsNode = settingsService.GetPaymentsNode();

            return paymentsNode != null ? new PaymentSettingsModel(paymentsNode) : null;
        }
    }
}
