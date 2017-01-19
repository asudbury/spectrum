namespace Spectrum.Application.Customer.Controllers
{
    using Correspondence.Controllers;
    using Correspondence.Providers;
    using Model.Correspondence;
    using Model.Customer;
    using Providers;

    public class CustomerController : EventController
    {
        /// <summary>
        /// The customer provider.
        /// </summary>
        private readonly ICustomerProvider customerProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController" /> class.
        /// </summary>
        /// <param name="customerProvider">The customer provider.</param>
        public CustomerController(
            ICustomerProvider customerProvider,
            IEventProvider eventProvider)
            :base(eventProvider)
        {
            this.customerProvider = customerProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController"/> class.
        /// </summary>
        public CustomerController()
            : this(new CustomerProvider(), new EventProvider())
        {
        }

        /// <summary>
        /// The user has updated their email address.
        /// </summary>
        /// <param name="model">The model.</param>
        public void EmailAddressUpdated(UpdateEmailAddressModel model)
        {
            customerProvider.EmailAddressUpdated(model);

            EventModel eventModel = new EventModel(model.Guid, Event.CustomerEmailAddressUpdated);
            eventProvider.InsertEvent(eventModel);
        }

        /// <summary>
        /// The user updated their name.
        /// </summary>
        /// <param name="model">The model.</param>
        public void NameUpdated(UpdateNameModel model)
        {
            customerProvider.NameUpdated(model);

            EventModel eventModel = new EventModel(model.Guid, Event.CustomerNameUpdated);
            eventProvider.InsertEvent(eventModel);
        }
    }
}
