namespace Spectrum.Content.Customer.Services
{
    public interface IClientService
    {
        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <returns></returns>
        int GetClientId(
            int customerId,
            string name,
            string emailAddress);
    }
}