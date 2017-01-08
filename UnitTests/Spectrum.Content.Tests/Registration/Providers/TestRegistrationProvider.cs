namespace Spectrum.Content.Tests.Registration.Providers
{
    using Content.Registration.Models;
    using Content.Registration.Providers;
    using Content.Registration.ViewModels;
    using Moq;
    using NUnit.Framework;
    using Services;
    using TinyMessenger;
    using Umbraco.Core.Models;

    /// <summary>
    /// The TestRegistration Provider.
    /// </summary>
    [TestFixture]
    public class TestRegistrationProvider
    {
        /// <summary>
        /// The mock User Service.
        /// </summary>
        private Mock<IUserService> mockUserService;

        /// <summary>
        /// The mock messenger hub.
        /// </summary>
        private Mock<ITinyMessengerHub> mockMessengerHub;

        /// <summary>
        /// Setup the tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            mockUserService = new Mock<IUserService>();
            mockMessengerHub = new Mock<ITinyMessengerHub>();
        }

        /// <summary>
        /// Tests the handle register member exists.
        /// </summary>
        [Test]
        public void TestHandleRegisterMemberExists()
        {
            RegistrationProvider provider = new RegistrationProvider(mockUserService.Object, mockMessengerHub.Object);

            mockUserService.Setup(
                x => x.CreateUser(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>())).Returns((IMember)null);

            RegisteredUser registeredUser = provider.RegisterUser(new RegisterViewModel());

            Assert.AreEqual(registeredUser, null);
        }

        /// <summary>
        /// Tests the handle register member not exists.
        /// </summary>
        [Test]
        public void TestHandleRegisterMemberNotExists()
        {
            IMemberType memberType = new MemberType(1);

            Member member = new Member(memberType);

            RegistrationProvider provider = new RegistrationProvider(mockUserService.Object, mockMessengerHub.Object);

            mockUserService.Setup(
                x => x.CreateUser(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>())).Returns(member);

            RegisteredUser registeredUser = provider.RegisterUser(new RegisterViewModel());

            Assert.AreNotEqual(registeredUser, null);
        }

        /// <summary>
        /// Tests the check email in use true.
        /// </summary>
        [Test]
        public void TestCheckEmailInUseTrue()
        {
            mockUserService.Setup(x => x.GetUser(It.IsAny<string>())).Returns((IMember)null);

            RegistrationProvider provider = new RegistrationProvider(mockUserService.Object, mockMessengerHub.Object);

            bool result = provider.CheckEmailInUse("a@a.com");

            Assert.AreNotEqual(result, true);
        }

        /// <summary>
        /// Tests the check email in use false.
        /// </summary>
        [Test]
        public void TestCheckEmailInUseFalse()
        {
            IMemberType memberType = new MemberType(1);

            Member member = new Member(memberType);

            mockUserService.Setup(x => x.GetUser(It.IsAny<string>())).Returns(member);

            RegistrationProvider provider = new RegistrationProvider(mockUserService.Object, mockMessengerHub.Object);

            bool  result = provider.CheckEmailInUse("a@a.com");

            Assert.AreEqual(result, true);
        }
    }
}
