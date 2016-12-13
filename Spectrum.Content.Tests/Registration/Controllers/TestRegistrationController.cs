namespace Spectrum.Content.Tests.Registration.Controllers
{
    using Content.Registration.Controllers;
    using Content.Registration.Providers;
    using Content.Registration.ViewModels;
    using GDev.Umbraco.Testing;
    using Moq;
    using NUnit.Framework;
    using System.Web.Mvc;

    /// <summary>
    /// The TestRegistration Contoller.
    /// </summary>
    [TestFixture]
    public class TestRegistrationController
    {
        /// <summary>
        /// The context mocker.
        /// </summary>
        private ContextMocker contextMocker;

        /// <summary>
        /// The mock Registration Provider.
        /// </summary>
        private Mock<IRegistrationProvider> mockRegistrationProvider;

        /// <summary>
        /// Setup the tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            contextMocker = new ContextMocker();
            mockRegistrationProvider = new Mock<IRegistrationProvider>();
        }

        /// <summary>
        /// Determines whether the controller has be initialized.
        /// </summary>
        [Test]
        public void TestCanInitializeController()
        {
            Assert.DoesNotThrow(() => new RegistrationController(contextMocker.UmbracoContextMock, mockRegistrationProvider.Object));
        }

        /// <summary>
        /// Tests the render register.
        /// </summary>
        [Test]
        public void TestRenderRegister()
        {
            RegistrationController controller = new RegistrationController(contextMocker.UmbracoContextMock, mockRegistrationProvider.Object);

            ActionResult actionResult = controller.RenderRegister();

            PartialViewResult partialViewResult = (PartialViewResult)actionResult;

            RegisterViewModel model = (RegisterViewModel)partialViewResult.Model;
            
            Assert.AreEqual(partialViewResult.ViewName, "Register");
            Assert.IsNotNull(model);
        }

        /// <summary>
        /// Tests the view model is invalid.
        /// </summary>
        [Test]
        public void TestHandleRegisterViewModelInvalid()
        {
            RegisterViewModel viewModel = new RegisterViewModel();
            
            RegistrationController controller = new RegistrationController(contextMocker.UmbracoContextMock, mockRegistrationProvider.Object);

            controller.ModelState.AddModelError("Email", "Email is required.");

            ActionResult actionResult = controller.HandleRegister(viewModel);

            PartialViewResult partialViewResult = (PartialViewResult)actionResult;

            RegisterViewModel model = (RegisterViewModel)partialViewResult.Model;

            Assert.AreEqual(model, null);
        }

        /// <summary>
        /// Tests the handle register.
        /// </summary>
        [Test]
        public void TestHandleRegister()
        {
            RegisterViewModel viewModel = new RegisterViewModel();

            RegistrationController controller = new RegistrationController(contextMocker.UmbracoContextMock, mockRegistrationProvider.Object);

            ActionResult actionResult = controller.HandleRegister(viewModel);

            PartialViewResult partialViewResult = (PartialViewResult)actionResult;

            RegisterViewModel model = (RegisterViewModel)partialViewResult.Model;

            Assert.AreEqual(partialViewResult.ViewName, "Register");
            Assert.IsNotNull(model);
            Assert.AreEqual(controller.ModelState.Count, 0);
        }

        /// <summary>
        /// Tests the check email in use true.
        /// </summary>
        [Test]
        public void TestCheckEmailInUseTrue()
        {
            mockRegistrationProvider.Setup(x => x.CheckEmailInUse(It.IsAny<string>())).Returns((true));

            RegistrationController controller = new RegistrationController(contextMocker.UmbracoContextMock, mockRegistrationProvider.Object);

            JsonResult result = controller.CheckEmailInUse("a@a.com");

            Assert.AreNotEqual(result.Data, true);
        }

        /// <summary>
        /// Tests the check email in use false.
        /// </summary>
        [Test]
        public void TestCheckEmailInUseFalse()
        {
            mockRegistrationProvider.Setup(x => x.CheckEmailInUse(It.IsAny<string>())).Returns((false));

            RegistrationController controller = new RegistrationController(contextMocker.UmbracoContextMock, mockRegistrationProvider.Object);

            JsonResult result = controller.CheckEmailInUse("a@a.com");

            Assert.AreEqual(result.Data, true);
        }
    }
}
