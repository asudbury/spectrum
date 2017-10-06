﻿namespace Spectrum.Content.Payments.Managers
{
    using System.Collections.Specialized;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using ViewModels;

    public interface IPaymentManager
    {
        /// <summary>
        /// Gets the take payment view model.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="queryStringParameters">The query string parameters.</param>
        /// <returns></returns>
        TakePaymentViewModel GetTakePaymentViewModel(
            UmbracoContext umbracoContext,
            NameValueCollection queryStringParameters);
        
        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        string GetAuthToken(UmbracoContext umbracoContext);

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <returns></returns>
        string GetEnvironment(UmbracoContext umbracoContext);

        /// <summary>
        /// Handles the payment.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="publishedContent">Content of the published.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        string HandlePayment(
            UmbracoContext umbracoContext,
            IPublishedContent publishedContent,
            PaymentViewModel viewModel);
    }
}
