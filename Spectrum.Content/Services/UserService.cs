namespace Spectrum.Content.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Security;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using Umbraco.Web.Security;

    /// <summary>
    /// The UserService class.
    /// </summary>
    /// <seealso cref="Spectrum.Content.Services.IUserService" />
    public class UserService : IUserService
    {
        /// <summary>
        /// The membership helper.
        /// </summary>
        private MembershipHelper membershipHelper;

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the member service.
        /// </summary>
        public IMemberService MemberService { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="password">The password.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="memberType">Type of the member.</param>
        /// <returns>An IMember.</returns>
        public IMember CreateUser(
            string name,
            string password,
            string emailAddress,
            string memberType)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name not supplied", name);
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password not supplied", password);
            }

            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentException("Email Address not supplied", emailAddress);
            }

            if (MemberService == null)
            {
                throw new ApplicationException("MemberService not set");
            }

            if (MemberService.GetByEmail(emailAddress) != null)
            {
                return null;
            }
            
            IMember member = MemberService.CreateMemberWithIdentity(emailAddress, emailAddress, name, memberType);

            member.IsApproved = false;

            ////member.SetValueIfHasProperty(UserConstants.HasVerifiedEmail, false);
            ////member.SetValueIfHasProperty(UserConstants.ProfileUrl, member.Id);
            ////member.SetValueIfHasProperty(UserConstants.RegistrationDate, DateTime.Now);

            MemberService.Save(member);
            MemberService.SavePassword(member, password);

            return member;
        }

        /// <inheritdoc />
        /// <summary>
        /// Determines whether [is user logged in].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is user logged in]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUserLoggedIn()
        {
            return GetMembershipHelper().IsLoggedIn();
        }

        /// <inheritdoc />
        /// <summary>
        /// Logins the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>True or False.</returns>
        public bool Login(
            string userName,
            string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Username not supplied", userName);
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password not supplied", password);
            }

            return GetMembershipHelper().Login(userName, password);
        }

        /// <inheritdoc />
        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public void Logout()
        {
            GetMembershipHelper().Logout();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>An IMember.</returns>
        public IMember GetUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Username not supplied", userName);
            }

            if (MemberService == null)
            {
                throw new ApplicationException("MemberService not set");
            }

            return MemberService.GetByUsername(userName);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates the login status.
        /// </summary>
        /// <param name="member">The member.</param>
        public void UpdateLoginStatus(IMember member)
        {
            if (member == null)
            {
                throw new ArgumentException("Member not supplied");
            }

            /*string hostName = Dns.GetHostName();
            string ipAddress = Dns.GetHostAddresses(hostName).GetValue(0).ToString();

            if (member.HasProperty(UserConstants.NumberOfLogins))
            {
                int noLogins = member.GetValue<int>(UserConstants.NumberOfLogins);
                member.SetValueIfHasProperty(UserConstants.NumberOfLogins, noLogins + 1);
            }
            */
            ////member.SetValueIfHasProperty(UserConstants.LastLoggedInDateTime, DateTime.Now);
            ////member.SetValueIfHasProperty(UserConstants.HostNameOfLastLogin, hostName);
            ////member.SetValueIfHasProperty(UserConstants.IpAddressOfLastLogin, ipAddress);

            MemberService.Save(member);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the user unique identifier.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns> The Guid.</returns>
        public Guid GetUserGuid(IMember member)
        {
            if (member == null)
            {
                throw new ArgumentException("Member not supplied");
            }

            return member.Key;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the default role.
        /// </summary>
        /// <returns></returns>
        public string GetDefaultRole()
        {
            string[] roles = Roles.GetRolesForUser();
            
            return roles.Length > 0 ? roles[0] : string.Empty;
        }

        /// <summary>
        /// Gets the default role.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns></returns>
        public string GetDefaultRole(string member)
        {
            List<string> memberRoles = Roles.GetRolesForUser(member).ToList();

            if (memberRoles.Count > 0)
            {
                return memberRoles.First();
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the membership helper.
        /// </summary>
        /// <returns>the MembershipHelper.</returns>
        internal MembershipHelper GetMembershipHelper()
        {
            if (membershipHelper == null)
            {
                membershipHelper = new MembershipHelper(Umbraco.Web.UmbracoContext.Current);
            }

            return membershipHelper;
        }
    }
}
