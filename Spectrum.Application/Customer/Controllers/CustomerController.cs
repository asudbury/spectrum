namespace Spectrum.Application.Customer.Controllers
{
    using Correspondence.Controllers;
    using Correspondence.Providers;
    using Core.Services;
    using Model.Correspondence;
    using Model.Customer;
    using Providers;

    public class CustomerController : EventController
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The customer provider.
        /// </summary>
        private readonly ICustomerProvider customerProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="customerProvider">The customer provider.</param>
        /// <param name="eventProvider">The event provider.</param>
        public CustomerController(
            ISettingsService settingsService,
            ICustomerProvider customerProvider,
            IEventProvider eventProvider)
            :base(eventProvider)
        {
            this.settingsService = settingsService;
            this.customerProvider = customerProvider;
        }

        /// <summary>
        /// The user has updated their email address.
        /// </summary>
        /// <param name="model">The model.</param>
        public void EmailAddressUpdated(UpdateEmailAddressModel model)
        {
            if (settingsService.IsCustomerEnabled)
            { 
                customerProvider.EmailAddressUpdated(model);

                EventModel eventModel = new EventModel(model.Guid, Event.CustomerEmailAddressUpdated);
                EventProvider.InsertEvent(eventModel);
            }
        }

        /// <summary>
        /// The user updated their name.
        /// </summary>
        /// <param name="model">The model.</param>
        public void NameUpdated(UpdateNameModel model)
        {
            if (settingsService.IsCustomerEnabled)
            {
                customerProvider.NameUpdated(model);

                EventModel eventModel = new EventModel(model.Guid, Event.CustomerNameUpdated);
                EventProvider.InsertEvent(eventModel);
            }
        }
    }
}

