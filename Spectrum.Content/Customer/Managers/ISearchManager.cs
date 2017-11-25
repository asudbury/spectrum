namespace Spectrum.Content.Customer.Managers
{
    using Models;
    using System.Collections.Generic;

    public interface ISearchManager
    {
        /// <summary>
        /// Gets the addresses.
        /// </summary>
        /// <param name="postCode">The post code.</param>
        /// <param name="buildingNumber">The building number.</param>
        /// <returns></returns>
        IEnumerable<AddressModel> GetAddresses(
            string postCode,
            string buildingNumber);
    }
}