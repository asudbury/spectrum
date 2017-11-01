
namespace Spectrum.Content.Services
{
    using System;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;

    /// <summary>
    /// The IUserService interface.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets or sets the member service.
        /// </summary>
        /// <value>
        /// The member service.
        /// </value>
        IMemberService MemberService { get; set; }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="password">The password.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="memberType">Type of the member.</param>
        /// <returns>An IMember.</returns>
        IMember CreateUser(
            string name,
            string password,
            string emailAddress,
            string memberType);

        /// <summary>
        /// Determines whether [is user logged in].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is user logged in]; otherwise, <c>false</c>.
        /// </returns>
        bool IsUserLoggedIn();

        /// <summary>
        /// Logins the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>True or False.</returns>
        bool Login(
            string userName,
            string password);

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        void Logout();

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>An IMember.</returns>
        IMember GetUser(string userName);

        /// <summary>
        /// Updates the login status.
        /// </summary>
        /// <param name="member">The member.</param>
        void UpdateLoginStatus(IMember member);

        /// <summary>
        /// Gets the user unique identifier.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns>The Guid.</returns>
        Guid GetUserGuid(IMember member);

        /// <summary>
        /// Gets the default role.
        /// </summary>
        /// <returns></returns>
        string GetDefaultRole();

        /// <summary>
        /// Gets the default role.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns></returns>
        string GetDefaultRole(string member);

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <returns></returns>
        string GetCurrentUserName();
    }
}
