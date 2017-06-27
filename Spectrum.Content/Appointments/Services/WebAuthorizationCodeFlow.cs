namespace Spectrum.Content.Appointments.Services
{
    using Google.Apis.Auth.OAuth2.Flows;
    using Google.Apis.Auth.OAuth2.Requests;

    public class WebAuthorizationCodeFlow : GoogleAuthorizationCodeFlow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebAuthorizationCodeFlow"/> class.
        /// </summary>
        /// <param name="initializer"></param>
        public WebAuthorizationCodeFlow(Initializer initializer) 
            : base(initializer)
        {
        }

        /// <summary>
        /// Creates the authorization code request.
        /// </summary>
        /// <param name="redirectUri">The redirect URI.</param>
        /// <returns></returns>
        public override AuthorizationCodeRequestUrl CreateAuthorizationCodeRequest(string redirectUri)
        {
            return base.CreateAuthorizationCodeRequest(WebAuthorizationBroker.RedirectUri);
        }
    }
}