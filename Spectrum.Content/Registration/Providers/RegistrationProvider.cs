namespace Spectrum.Content.Registration.Providers
{
    using Model.Correspondence;
    using Model.Registration;
    using Models;
    using System;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using ViewModels;

    /// <summary>
    /// The RegistrationProvider class.
    /// </summary>
    /// <seealso cref="Spectrum.Content.Registration.Providers.IRegistrationProvider" />
    internal class RegistrationProvider : IRegistrationProvider
    {
        /// <summary>
        /// The user service.
        /// </summary>
        private readonly Services.IUserService userService;

        /// <summary>
        /// Gets or sets the member service.
        /// </summary>
        public IMemberService MemberService { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationProvider"/> class.
        /// </summary>
        internal RegistrationProvider() 
            : this(new Services.UserService(), 
              TinyIoC.TinyIoCContainer.Current.Resolve<ITinyMessengerHub>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationProvider"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="tinyMessengerHub"></param>
        internal RegistrationProvider(
            Services.IUserService userService,
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
            //// TODO : we need to extract the guid from the token
            
            NotificationModel model = new NotificationModel(new Guid());

            tinyMessengerHub.Publish(new UserVerificationCompleteMessage(this, model));

            return true;
        }
    }
}
