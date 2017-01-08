
namespace Spectrum.Application.Authentication.Repositories
{
    using Model;

    internal class PasswordRepository : IPasswordRepository
    {
        /// <summary>
        /// Password reset requested.
        /// </summary>
        /// <param name="model">The model.</param>
        public void ResetRequested(NotificationModel model)
        {
        }

        /// <summary>
        /// Password Reset Completed.
        /// </summary>
        /// <param name="model">The model.</param>
        public void ResetCompleted(NotificationModel model)
        {
        }
    }
}
