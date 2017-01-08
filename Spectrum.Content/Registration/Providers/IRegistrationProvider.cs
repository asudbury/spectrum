namespace Spectrum.Content.Registration.Providers
{
    using Models;
    using Umbraco.Core.Services;
    using ViewModels;

    /// <summary>
    /// The IRegistrationProvider interface.
    /// </summary>
    public interface IRegistrationProvider
    {
        /// <summary>
        /// Gets or sets the member service.
        /// </summary>
        IMemberService MemberService { get; set; }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>A RegisteredUser.</returns>
        RegisteredUser RegisterUser(RegisterViewModel viewModel);

        /// <summary>
        /// Verifies the user.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>True or False.</returns>
        bool VerifyUser(VerifyUserViewModel viewModel);

        /// <summary>
        /// Checks the email in use.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns>True or False.</returns>
        bool CheckEmailInUse(string emailAddress);
    }
}
