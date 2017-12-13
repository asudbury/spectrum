namespace Spectrum.Content.Customer.Services
{
    using Models;

    public interface IClientService
    {
        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="addressId">The address identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <returns></returns>
        int GetClientId(
            int customerId,
            int addressId,
            string name,
            string emailAddress);

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        ClientModel GetClient(int id);

        /// <summary>
        /// Creates the client.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        int CreateClient(ClientModel model);

        /// <summary>
        /// Updates the client.
        /// </summary>
        /// <param name="model">The model.</param>
        void UpdateClient(ClientModel model);
    }
}