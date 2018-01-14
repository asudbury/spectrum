namespace Spectrum.Content.Customer.Services
{
    using Content.Services;
    using Models;
    using System;
    using System.Collections.Generic;
    using Umbraco.Core;
    using Umbraco.Core.Persistence;

    public class ClientService : IClientService
    {
        /// <summary>
        /// The user service.
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public ClientService(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Gets the client id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="addressId">The address identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <returns></returns>
        public int GetClientId(
            int customerId,
            int addressId,
            string name,
            string emailAddress)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            Sql sql = new Sql()
                .Select("*")
                .From(Content.Constants.Database.ClientTableName)
                .Where("AddressId=" + addressId + " and CustomerId=" + customerId);

            ClientModel model = context.Database.FirstOrDefault<ClientModel>(sql);

            if (model != null)
            {
                model.EmailAddress = emailAddress;
                model.Name = name;
                model.LastUpdatedTime = DateTime.Now;
                model.LastUpdatedUser = userService.GetCurrentUserName();

                UpdateClient(model);

                return model.Id;
            }

            model = new ClientModel
            {
                CreatedTime = DateTime.Now,
                CreatedUser = userService.GetCurrentUserName(),
                LastUpdatedTime = DateTime.Now,
                LastUpdatedUser = userService.GetCurrentUserName(),
                CustomerId = customerId,
                EmailAddress = emailAddress,
                Name = name
            };

            return CreateClient(model);
        }

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ClientModel GetClient(
            int customerId,
            int id)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            Sql sql = new Sql()
                .Select("*")
                .From(Content.Constants.Database.ClientTableName)
                .Where("CustomerId=" + customerId + "and Id=" + id);

            return context.Database.FirstOrDefault<ClientModel>(sql);
        }

        /// <summary>
        /// Creates the client.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int CreateClient(ClientModel model)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            object clientId = context.Database.Insert(model);

            return Convert.ToInt32(clientId);
        }

        /// <summary>
        /// Updates the client.
        /// </summary>
        /// <param name="model">The model.</param>
        public void UpdateClient(ClientModel model)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            context.Database.Update(model);
        }

        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public IEnumerable<ClientModel> GetClients(int customerId)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            Sql sql = new Sql()
                .Select("*")
                .From(Content.Constants.Database.ClientTableName)
                .Where("CustomerId=" + customerId)
                .OrderBy("Name");
            return context.Database.Fetch<ClientModel>(sql);
        }
    }
}
