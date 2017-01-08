namespace Spectrum.Application
{
    public enum Process
    {
        /// <summary>
        /// The user registered.
        /// </summary>
        UserRegistered,

        /// <summary>
        /// The user verified.
        /// </summary>
        UserVerified,

        /// <summary>
        /// The login completed.
        /// </summary>
        LoginComplete,

        /// <summary>
        /// The login failed.
        /// </summary>
        LoginFailed,

        /// <summary>
        /// The user locked out.
        /// </summary>
        UserLockedOut,

        /// <summary>
        /// The password reset requested.
        /// </summary>
        PasswordResetRequested,

        /// <summary>
        /// The password reset completed.
        /// </summary>
        PasswordResetCompleted,

        /// <summary>
        /// The customer email address updated.
        /// </summary>
        CustomerEmailAddressUpdated,

        /// <summary>
        /// The customer name updated.
        /// </summary>
        CustomerNameUpdated,

        /// <summary>
        /// The email sent.
        /// </summary>
        EmailSent,

        /// <summary>
        /// The email read
        /// </summary>
        EmailRead
    }
}
