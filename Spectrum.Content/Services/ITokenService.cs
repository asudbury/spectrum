namespace Spectrum.Content.Services
{
    using ContentModels;

    using System.Collections.Generic;

    public interface ITokenService
    {
        /// <summary>
        /// Gets the base tokens.
        /// </summary>
        /// <param name="customerModel">The customer model.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <returns></returns>
        Dictionary<string, string> GetBaseTokens(
            CustomerModel customerModel,
            string clientName);
    }
}