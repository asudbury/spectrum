namespace Spectrum.Content.Authentication.Managers
{
    using Services;
    using ViewModels;

    public interface ILoginManager
    {
        /// <summary>
        /// Gets the user service.
        /// </summary>
        IUserService UserService { get; set; }

        /// <summary>
        /// Gets the login view model.
        /// </summary>
        /// <returns></returns>
        LoginViewModel GetLoginViewModel();

        /// <summary>
        /// Determines whether [is user logged in].
        /// </summary>
        bool IsUserLoggedIn();

        /// <summary>
        /// Logins the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        bool Login(
            string userName,
            string password);

        /// <summary>
        /// Logs the out.
        /// </summary>
        void LogOut();

        /// <summary>
        /// Sets the cookies.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="isLocalHost">if set to <c>true</c> [is local host].</param>
        void SetCookies(
            LoginViewModel viewModel,
            bool isLocalHost);

        /// <summary>
        /// Gets the return URL.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns></returns>
        string GetReturnUrl(string emailAddress);
    }
}