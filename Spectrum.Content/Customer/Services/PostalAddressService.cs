namespace Spectrum.Content.Customer.Services
{
    using HtmlAgilityPack;
    using Models;
    using ScrapySharp.Extensions;
    using System.Collections.Generic;
    using System.Linq;

    public class PostalAddressService : IPostalAddressService
    {
        /// <summary>
        /// The URL.
        /// </summary>
        private const string Url = "https://address-data.co.uk/addresses-in-";

        /// <summary>
        /// The CSS select command.
        /// </summary>
        private const string CssSelectCommand = ".offer-content p";

        /// <inheritdoc />
        /// <summary>
        /// Gets the addresses from post code.
        /// </summary>
        /// <param name="postCode">The post code.</param>
        /// <returns></returns>
        public IEnumerable<AddressModel> GetAddressesFromPostCode(string postCode)
        {
            postCode = postCode.ToUpper();

            string searchPostCode = postCode.Replace(" ", "-");

            List<AddressModel> addresses = new List<AddressModel>();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(Url + searchPostCode);

            var nodes = doc.DocumentNode.CssSelect(CssSelectCommand).ToList();

            foreach (HtmlNode htmlNode in nodes)
            {
                string addressString = htmlNode.InnerText;

                string buildingNumber = string.Concat(addressString.TakeWhile(char.IsLetterOrDigit));

                AddressModel addressModel = new AddressModel
                {
                    BuildingNumber = buildingNumber,
                    FullAddress = addressString,
                    PostCode = postCode
                };

                addresses.Add(addressModel);
            }

            return addresses.OrderBy(x => x.FullAddress);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the addresses from post code and building number.
        /// </summary>
        /// <param name="postCode">The post code.</param>
        /// <param name="buildingNumber">The building number.</param>
        /// <returns></returns>
        public IEnumerable<AddressModel> GetAddressesFromPostCodeAndBuildingNumber(
            string postCode, 
            string buildingNumber)
        {
            List<AddressModel> addresses = GetAddressesFromPostCode(postCode).ToList();

            List<AddressModel> buildingNumberAddresses = addresses.Where(x => x.BuildingNumber == buildingNumber).ToList();

            if (buildingNumberAddresses.Any())
            {
                return buildingNumberAddresses;
            }

            return addresses;
        }

        /// <summary>
        /// Gets the address identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="postCode">The post code.</param>
        /// <param name="buildingNumber">The building number.</param>
        /// <param name="fullAddress">The full address.</param>
        /// <returns></returns>
        public int GetAddressId(
            int customerId,
            string postCode, 
            string buildingNumber,
            string fullAddress)
        {
            return 0;
        }
    }
}
