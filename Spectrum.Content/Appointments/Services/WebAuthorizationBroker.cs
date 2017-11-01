using Google.Apis.Auth.OAuth2.Responses;
using Microsoft.Owin.Security.OAuth;

namespace Spectrum.Content.Appointments.Services
{
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Auth.OAuth2.Flows;
    using Google.Apis.Util.Store;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class WebAuthorizationBroker : GoogleWebAuthorizationBroker
    {
        /// <summary>
        /// The redirect URI.
        /// </summary>
        public static string RedirectUri;

        /// <summary>
        /// Authorizes the asynchronous.
        /// </summary>
        /// <param name="clientSecrets">The client secrets.</param>
        /// <param name="scopes">The scopes.</param>
        /// <param name="user">The user.</param>
        /// <param name="taskCancellationToken">The task cancellation token.</param>
        /// <param name="dataStore">The data store.</param>
        /// <returns></returns>
        public static async Task<UserCredential> AuthorizeAsync(
            ClientSecrets clientSecrets,
            IEnumerable<string> scopes,
            string user,
            CancellationToken taskCancellationToken,
            IDataStore dataStore = null)
        {
            GoogleAuthorizationCodeFlow.Initializer initializer = new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = clientSecrets,
            };

            return await AuthorizeAsyncCore(
                initializer, 
                scopes, 
                user,
                taskCancellationToken, 
                dataStore).ConfigureAwait(false);
        }

        /// <summary>
        /// Authorizes the asynchronous core.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        /// <param name="scopes">The scopes.</param>
        /// <param name="user">The user.</param>
        /// <param name="taskCancellationToken">The task cancellation token.</param>
        /// <param name="dataStore">The data store.</param>
        /// <returns></returns>
        private static async Task<UserCredential> AuthorizeAsyncCore(
            GoogleAuthorizationCodeFlow.Initializer initializer,
            IEnumerable<string> scopes,
            string user,
            CancellationToken taskCancellationToken,
            IDataStore dataStore)
        {
            initializer.Scopes = scopes;
            initializer.DataStore = dataStore ?? new FileDataStore(Folder);

            WebAuthorizationCodeFlow flow = new WebAuthorizationCodeFlow(initializer);

            TokenResponse tokenResponse = flow.ExchangeCodeForTokenAsync(user, "", "postmessage", CancellationToken.None).Result; 
            
            return new UserCredential(flow, "me", tokenResponse);

            /*return await new AuthorizationCodeInstalledApp(flow,
                new LocalServerCodeReceiver())
                .AuthorizeAsync(user, taskCancellationToken).ConfigureAwait(false);*/
        }

    }
}
