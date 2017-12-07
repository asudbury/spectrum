namespace Spectrum.Content.Services
{
    public interface IRulesEngineService
    {
        /// <summary>
        /// Determines whether [is customer appointments enabled].
        /// </summary>
        bool IsCustomerAppointmentsEnabled();

        /// <summary>
        /// Determines whether [is customer payments enabled].
        /// </summary>
        bool IsCustomerPaymentsEnabled();

        /// <summary>
        /// Determines whether [is customer dashboard enabled].
        /// </summary>
        bool IsCustomerDashboardEnabled();

        /// <summary>
        /// Determines whether [is customer google calendar enabled].
        /// </summary>
        bool IsCustomerGoogleCalendarEnabled();

        /// <summary>
        /// Determines whether [is customer invoices enabled] [the specified umbraco context].
        /// </summary>
        bool IsCustomerInvoicesEnabled();

        /// <summary>
        /// Determines whether [is customer quotes enabled] [the specified umbraco context].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is customer quotes enabled]; otherwise, <c>false</c>.
        /// </returns>
        bool IsCustomerQuotesEnabled();

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        bool Execute(string query);
    }
}
