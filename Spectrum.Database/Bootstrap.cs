namespace Spectrum.Database.Bootstrap
{
    using Registration.Controllers;

    public static class Bootstrap
    {
        public static void Start()
        {
            RegistrationController registrationController = new RegistrationController();

            registrationController.Bootstrap();
        }
    }
}
