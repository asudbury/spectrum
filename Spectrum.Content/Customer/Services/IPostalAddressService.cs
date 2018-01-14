namespace Spectrum.Content.Customer.Services
{
    using Models;
    using System.Collections.Generic;

    public interface IPostalAddressService
    {
        /// <summary>
        /// Gets the addresses from post code.
        /// </summary>
        /// <param name="postCode">The post code.</param>
        /// <returns></returns>
        IEnumerable<AddressModel> GetAddressesFromPostCode(string postCode);

        /// <summary>
        /// Gets the addresses from post code and building number.
        /// </summary>
        /// <param name="postCode">The post code.</param>
        /// <param name="buildingNumber">The building number.</param>
        /// <returns></returns>
        IEnumerable<AddressModel> GetAddressesFromPostCodeAndBuildingNumber(
            string postCode,
            string buildingNumber);
    }
}