namespace Spectrum.Content.Services
{
    using ContentModels;
    using System.Collections.Generic;

    public class TokenService : ITokenService
    {
        /// <summary>
        /// Gets the base tokens.
        /// </summary>
        /// <param name="customerModel">The customer model.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <returns></returns>
        public Dictionary<string, string> GetBaseTokens(
            CustomerModel customerModel,
           string  clientName)
        {
            return new Dictionary<string, string>
            {
                {"ClientName", clientName},
                {"CustomerName", customerModel.Name},
                {"CustomerAddress", customerModel.Address}
            };
        }
    }
}
