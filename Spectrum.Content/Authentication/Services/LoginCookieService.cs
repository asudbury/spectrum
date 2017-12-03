namespace Spectrum.Content.Authentication.Services
{
    using Application.Services;
    using Content.Services;
    using ContentModels;
    using System;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class LoginCookieService : ILoginCookieService
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The cookie service.
        /// </summary>
        private readonly ICookieService cookieService;

        /// <summary>
        /// The encryption service
        /// </summary>
        private readonly IEncryptionService encryptionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginCookieService" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="cookieService">The cookie service.</param>
        /// <param name="encryptionService">The encryption service.</param>
        public LoginCookieService(
            ISettingsService settingsService,
            ICookieService cookieService,
            IEncryptionService encryptionService)
        {
            this.settingsService = settingsService;
            this.cookieService = cookieService;
            this.encryptionService = encryptionService;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Sets the cookies.
        /// </summary>
        /// <param name="umbracoContext">The umbraco context.</param>
        /// <param name="isLocalHost">if set to <c>true</c> [is local host].</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="rememberMe">if set to <c>true</c> [remember me].</param>
        /// <param name="password">The password.</param>
        /// <inheritdoc />
        public void SetCookies(
            UmbracoContext umbracoContext,
            bool isLocalHost,
            string emailAddress, 
            bool rememberMe, 
            string password)
        {
            string emailKey = AuthenticationConstants.EmailAddress;
            string rememberMeKey = AuthenticationConstants.RememberMe;
            string passwordKey = AuthenticationConstants.Password;

            if (rememberMe)
            {
                string encryptedEmailAddress = encryptionService.EncryptString(emailAddress);
                cookieService.SetValue(emailKey, encryptedEmailAddress);

                string encryptedRememberMe = encryptionService.EncryptString(true.ToString());
                cookieService.SetValue(rememberMeKey, encryptedRememberMe);

                IPublishedContent customerNode = settingsService.GetCustomerNode(umbracoContext);

                CustomerModel model = new CustomerModel(customerNode);
                
                if (isLocalHost || model.SavePassword)
                {
                    string encryptedPassword = encryptionService.EncryptString(password);
                    cookieService.SetValue(passwordKey, encryptedPassword);
                }

                else
                {
                    cookieService.Expire(passwordKey);
                }
            }

            else
            {
                cookieService.Expire(emailKey);
                cookieService.Expire(passwordKey);
                cookieService.Expire(rememberMeKey);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Ges the email adress.
        /// </summary>
        /// <returns></returns>
        public string GeEmailAdress()
        {
            string emailAddress = cookieService.GetValue(AuthenticationConstants.EmailAddress);

            return string.IsNullOrEmpty(emailAddress) == false ? encryptionService.DecryptString(emailAddress) : string.Empty;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the remember me.
        /// </summary>
        /// <returns></returns>
        public bool GetRememberMe()
        {
            string rememberMe = cookieService.GetValue(AuthenticationConstants.RememberMe);

            if (string.IsNullOrEmpty(rememberMe) == false)
            {
                string decryptString = encryptionService.DecryptString(rememberMe);

                return Convert.ToBoolean(decryptString);
            }

            return false;
        }

        /// <inheritdoc />
        /// <summary>
        /// Passwords this instance.
        /// </summary>
        /// <returns></returns>
        public string GetPassword()
        {
            string password = cookieService.GetValue(AuthenticationConstants.Password);

            return string.IsNullOrEmpty(password) == false ? encryptionService.DecryptString(password) : string.Empty;
        }
    }
}