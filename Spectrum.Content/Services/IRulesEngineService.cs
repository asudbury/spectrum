namespace Spectrum.Content.Services
{
    using Umbraco.Web;

    public interface IRulesEngineService
    {
        /// <summary>
        /// Determines whether [is customer appointments enabled].
        /// </summary>
        bool IsCustomerAppointmentsEnabled(UmbracoContext umbracoContext);

        /// <summary>
        /// Determines whether [is customer payments enabled].
        /// </summary>
        bool IsCustomerPaymentsEnabled(UmbracoContext umbracoContext);

        /// <summary>
        /// Determines whether [is customer dashboard enabled].
        /// </summary>
        bool IsCustomerDashboardEnabled(UmbracoContext umbracoContext);

        /// <summary>
        /// Determines whether [is customer google calendar enabled].
        /// </summary>
        bool IsCustomerGoogleCalendarEnabled(UmbracoContext umbracoContext);

        /// <summary>
        /// Determines whether [is customer invoices enabled] [the specified umbraco context].
        /// </summary>
        bool IsCustomerInvoicesEnabled(UmbracoContext umbracoContext);

        /// <summary>
        /// Determines whether [is customer quotes enabled] [the specified umbraco context].
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        bool IsCustomerQuotesEnabled(UmbracoContext umbracoContext);

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        bool Execute(
            UmbracoContext umbracoContext,
            string query);
    }
}
