
namespace Spectrum.Application.Authentication.Providers
{
    using Model;

    /// <summary>
    /// The ILoginProvider interface.
    /// </summary>
    public interface ILoginProvider
    {
        /// <summary>
        /// The user has logged in.
        /// </summary>
        /// <param name="model">The model.</param>
        void LoginComplete(NotificationModel model);

        /// <summary>
        /// The user login has failed.
        /// </summary>
        /// <param name="model">The model.</param>
        void LoginFailed(NotificationModel model);
    }
}
