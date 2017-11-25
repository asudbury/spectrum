namespace Spectrum.Content.Customer.Managers
{
    using Models;
    using Services;
    using System.Collections.Generic;

    public class SearchManager : ISearchManager
    {
        /// <summary>
        /// The postal address service.
        /// </summary>
        private readonly IPostalAddressService postalAddressService;

        public SearchManager(IPostalAddressService postalAddressService)
        {
            this.postalAddressService = postalAddressService;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the addresses.
        /// </summary>
        /// <param name="postCode">The post code.</param>
        /// <param name="buildingNumber">The building number.</param>
        /// <returns></returns>
        public IEnumerable<AddressModel> GetAddresses(
            string postCode, string 
            buildingNumber)
        {
            if (string.IsNullOrEmpty(buildingNumber))
            {
                return postalAddressService.GetAddressesFromPostCode(postCode);
            }

            return postalAddressService.GetAddressesFromPostCodeAndBuildingNumber(
                                            postCode,
                                            buildingNumber);
         
        }
    }
}
