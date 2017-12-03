namespace Spectrum.Content.Authentication.Services
{
    using Umbraco.Web;

    public interface  ILoginCookieService
    {
        /// <summary>
        /// Sets the cookies.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="isLocalHost">if set to <c>true</c> [is local host].</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="rememeberMe">if set to <c>true</c> [rememeber me].</param>
        /// <param name="password">The password.</param>
        void SetCookies(
            UmbracoContext umbracoContext,
            bool isLocalHost,
            string emailAddress, 
            bool rememeberMe, 
            string password);

        /// <summary>
        /// Ges the email adress.
        /// </summary>
        /// <returns></returns>
        string GeEmailAdress();

        /// <summary>
        /// Gets the remember me.
        /// </summary>
        /// <returns></returns>
        bool GetRememberMe();

        /// <summary>
        /// Gets the Password.
        /// </summary>
        /// <returns></returns>
        string GetPassword();
    }
}
