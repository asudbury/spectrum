namespace Spectrum.Application.Authentication.Repositories
{
    using Model;

    public interface IPasswordRepository
    {
        /// <summary>
        /// Password reset requested.
        /// </summary>
        /// <param name="model">The model.</param>
        void ResetRequested(NotificationModel model);

        /// <summary>
        /// Password Reset Completed.
        /// </summary>
        /// <param name="model">The model.</param>
        void ResetCompleted(NotificationModel model);
    }
}
