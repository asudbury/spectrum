
namespace Spectrum.Application.Authentication.Providers
{
    using Model;

    /// <summary>
    /// The IPasswordProvider interface.
    /// </summary>
    public interface IPasswordProvider
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
