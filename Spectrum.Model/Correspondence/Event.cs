namespace Spectrum.Model.Correspondence
{
    public enum Event
    {
        /// <summary>
        /// The user registered.
        /// </summary>
        UserRegistered = 1,

        /// <summary>
        /// The user verified.
        /// </summary>
        UserVerified = 2,

        /// <summary>
        /// The login completed.
        /// </summary>
        LoginComplete = 3,

        /// <summary>
        /// The login failed.
        /// </summary>
        LoginFailed = 4,

        /// <summary>
        /// The user locked out.
        /// </summary>
        UserLockedOut = 5,

        /// <summary>
        /// The password reset requested.
        /// </summary>
        PasswordResetRequested = 6,

        /// <summary>
        /// The password reset completed.
        /// </summary>
        PasswordResetCompleted = 7,

        /// <summary>
        /// The customer email address updated.
        /// </summary>
        CustomerEmailAddressUpdated = 8,

        /// <summary>
        /// The customer name updated.
        /// </summary>
        CustomerNameUpdated = 9,

        /// <summary>
        /// The email sent.
        /// </summary>
        EmailSent = 10,

        /// <summary>
        /// The email read
        /// </summary>
        EmailRead = 11
    }
}
