namespace Spectrum.Application.Authentication.Repositories
{
    using Model;

    public interface ILoginRepository
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

        /// <summary>
        /// Users the locked out.
        /// </summary>
        /// <param name="model">The model.</param>
        void UserLockedOut(NotificationModel model);
    }
}
