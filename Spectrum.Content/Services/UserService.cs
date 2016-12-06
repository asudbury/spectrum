namespace Spectrum.Content.Services
{
    using System;
    using System.Net;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using Umbraco.Web.Security;

    public class UserService : IUserService
    {
        private MembershipHelper membershipHelper;

        public IMemberService MemberService { get; set; }

        public IMember CreateUser(
            string name,
            string password,
            string emailAddress,
            string memberType)
        {
            Guid guid = Guid.NewGuid();

            if (MemberService.GetByEmail(emailAddress) != null)
            {
                return null;
            }

            IMember member = MemberService.CreateMemberWithIdentity(emailAddress, emailAddress, name, memberType);

            member.IsApproved = false;
            member.SetValue(UserConstants.HasVerifiedEmail, false);
            member.SetValue(UserConstants.ProfileUrl, member.Id);
            member.SetValue(UserConstants.Guid, guid.ToString());
            member.SetValue(UserConstants.RegistrationDate, DateTime.Now.ToString("dd/MM/yyyy @ HH:mm:ss"));

            MemberService.Save(member);
            MemberService.SavePassword(member, password);

            return member;
        }

        public bool IsUserLoggedIn()
        {
            return GetMembershipHelper().IsLoggedIn();
        }

        public bool Login(
            string userName,
            string password)
        {
            return GetMembershipHelper().Login(userName, password);
        }

        public void Logout()
        {
            GetMembershipHelper().Logout();
        }

        public IMember GetUser(string userName)
        {
            return MemberService.GetByUsername(userName);
        }

        public void UpdateLoginStatus(IMember member)
        {
            string hostName = Dns.GetHostName();
            string ipAddress = Dns.GetHostAddresses(hostName).GetValue(0).ToString();

            int noLogins = member.GetValue<int>(UserConstants.NumberOfLogins);

            member.SetValue(UserConstants.NumberOfLogins, noLogins + 1);
            member.SetValue(UserConstants.LastLoggedInDateTime, DateTime.Now.ToString("dd/MM/yyyy @ HH:mm:ss"));
            member.SetValue(UserConstants.HostNameOfLastLogin, hostName);
            member.SetValue(UserConstants.IpAddressOfLastLogin, ipAddress);

            MemberService.Save(member);
        }

        public string GetUserGuid(IMember member)
        {
            return member.GetValue<string>(UserConstants.Guid);
        }

        private MembershipHelper GetMembershipHelper()
        {
            if (membershipHelper == null)
            {
                membershipHelper = new MembershipHelper(Umbraco.Web.UmbracoContext.Current);
            }

            return membershipHelper;
        }
    }
}
