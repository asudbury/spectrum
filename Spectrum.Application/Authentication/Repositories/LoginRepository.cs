
namespace Spectrum.Application.Authentication.Repositories
{
    using Model;

    internal class LoginRepository : ILoginRepository
    {
        /// <summary>
        /// The user has logged in.
        /// </summary>
        /// <param name="model">The model.</param>
        public void LoginComplete(NotificationModel model)
        {
        }

        /// <summary>
        /// The user login has failed.
        /// </summary>
        /// <param name="model">The model.</param>
        public void LoginFailed(NotificationModel model)
        {
        }

        /// <summary>
        /// Users the locked out.
        /// </summary>
        /// <param name="model">The model.</param>
        public void UserLockedOut(NotificationModel model)
        {
        }
    }
}
