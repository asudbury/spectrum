using Umbraco.Core.Services;
using Umbraco.Web;

namespace Spectrum.Content.Registration.Providers
{
    using Model.Registration;
    using Models;
    using Services;
    using System;
    using ViewModels;
    using TinyMessenger;
    using Umbraco.Core.Models;

    /// <summary>
    /// The RegistrationProvider class.
    /// </summary>
    /// <seealso cref="Spectrum.Content.Registration.Providers.IRegistrationProvider" />
    internal class RegistrationProvider : IRegistrationProvider
    {
        /// <summary>
        /// The user service.
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The tiny messenger hub.
        /// </summary>
        private readonly ITinyMessengerHub tinyMessengerHub;

        /// <summary>
        /// Gets or sets the member service.
        /// </summary>
        public IMemberService MemberService { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationProvider"/> class.
        /// </summary>
        internal RegistrationProvider() 
            : this(new UserService(), 
              TinyIoC.TinyIoCContainer.Current.Resolve<ITinyMessengerHub>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationProvider"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="tinyMessengerHub"></param>
        internal RegistrationProvider(
            IUserService userService,
            ITinyMessengerHub tinyMessengerHub)
        {
            this.userService = userService;
            this.tinyMessengerHub = tinyMessengerHub;
        }

        /// <summary>
        /// Register the user.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>An RegisteredUser.</returns>
        public RegisteredUser RegisterUser(RegisterViewModel viewModel)
        {
            userService.MemberService = MemberService;

            IMember member = userService.CreateUser(
                viewModel.Name,
                viewModel.Password,
                viewModel.EmailAddress,
                viewModel.MemberType);

            if (member == null)
            {
                return null;
            }

            Guid guid = userService.GetUserGuid(member);

            RegisterModel model = new RegisterModel(
                viewModel.Name,
                viewModel.EmailAddress,
                guid);

            tinyMessengerHub.Publish(new RegistrationCompleteMessage(this, model));

            return new RegisteredUser(member, guid);
        }

        /// <summary>
        /// Checks the email is in use.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns>True or False.</returns>
        public bool CheckEmailInUse(string emailAddress)
        {
            userService.MemberService = MemberService;
            return userService.GetUser(emailAddress) != null;
        }

        /// <summary>
        /// Verifies the user.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>True or False.</returns>
        public bool VerifyUser(VerifyUserViewModel viewModel)
        {
            return true;
        }
    }
}
