namespace Spectrum.TestConsole
{
    using Application.DbUp;
    using Application.Authentication.Controllers;
    using Application.Correspondence.Controllers;
    using Application.Customer.Controllers;
    using Application.Registration.Controllers;
    using Model.Registration;
    using Model.Correspondence;
    using Model.Customer;
    using System;

    class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            //// change this to execute a different scenario!
            //Event eventType = Event.UserRegistered;
            //Event eventType = Event.UserVerified;

            //Call bootstrap database

            RegistrationDatabase registrationDatabase = new RegistrationDatabase();
            registrationDatabase.Update();

            Event eventType = Event.UserRegistered;

            switch (eventType)
            {
                case Event.UserRegistered:
                    UserRegistered();
                    break;

                case Event.UserVerified:
                    UserVerified();
                    break;

                case Event.LoginComplete:
                    LoginComplete();
                    break;

                case Event.LoginFailed:
                    LoginFailed();
                    break;

                case Event.UserLockedOut:
                    UserLockedOut();
                    break;

                case Event.PasswordResetRequested:
                    PasswordResetRequested();
                    break;

                case Event.PasswordResetCompleted:
                    PasswordResetCompleted();
                    break;

                case Event.CustomerEmailAddressUpdated:
                    CustomerEmailAddressUpdated();
                    break;

                case Event.CustomerNameUpdated:
                    CustomerNameUpdated();
                    break;

                case Event.EmailSent:
                    EmailSent();
                    break;

                case Event.EmailRead:
                    EmailRead();
                    break;
            }
        }

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        /// <returns></returns>
        static Guid GetGuid()
        {
            Guid guid = Guid.NewGuid();

            return guid;
        }

        /// <summary>
        /// Users the registered.
        /// </summary>
        static void UserRegistered()
        {
            RegistrationController controller = new RegistrationController();

            //// TODO : we might want to encrypt name and email - need to discuss!
            
            RegisterModel model = new RegisterModel("Mr David Test", "a@email.com", GetGuid());

            controller.UserRegistered(model);
        }

        /// <summary>
        /// Users the verified.
        /// </summary>
        static void UserVerified()
        {
            RegistrationController controller = new RegistrationController();

            NotificationModel model = new NotificationModel(GetGuid());

            controller.UserVerified(model);
        }

        static void LoginComplete()
        {
            LoginController controller = new LoginController();

            NotificationModel model = new NotificationModel(GetGuid());

            controller.LoginComplete(model);
        }
        
        static void LoginFailed()
        {
            LoginController controller = new LoginController();

            NotificationModel model = new NotificationModel(GetGuid());

            controller.LoginFailed(model);
        }

        static void UserLockedOut()
        {
        }

        static void PasswordResetRequested()
        {
            PasswordController controller = new PasswordController();

            NotificationModel model = new NotificationModel(GetGuid());

            controller.ResetRequested(model);
        }
        
        static void PasswordResetCompleted()
        {
            PasswordController controller = new PasswordController();

            NotificationModel model = new NotificationModel(GetGuid());

            controller.ResetCompleted(model);
        }
        
        static void CustomerEmailAddressUpdated()
        {
            CustomerController controller = new CustomerController();

            UpdateEmailAddressModel model = new UpdateEmailAddressModel("a@a.com", GetGuid());

            controller.EmailAddressUpdated(model);
        }
        
        static void CustomerNameUpdated()
        {
            CustomerController controller = new CustomerController();

            UpdateNameModel model = new UpdateNameModel("Mr A Smith", GetGuid());

            controller.NameUpdated(model);
        }
        
        static void EmailSent()
        {
            EmailController controller = new EmailController();

            EmailSentModel model = new EmailSentModel("1235", GetGuid());

            controller.EmailSent(model);
        }

        static void EmailRead()
        {
            EmailController controller = new EmailController();

            EmailSentModel model = new EmailSentModel("1235", GetGuid());

            controller.EmailSent(model);
        }
    }
}
