namespace Spectrum.Content.Services
{
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;

    public interface IUserService
    {
        IMemberService MemberService { get; set; }

        IMember CreateUser(
            string name,
            string password,
            string emailAddress,
            string memberType);

        bool IsUserLoggedIn();

        bool Login(
            string userName,
            string password);

        void Logout();

        IMember GetUser(string userName);

        void UpdateLoginStatus(IMember member);

        string GetUserGuid(IMember member);
    }
}
