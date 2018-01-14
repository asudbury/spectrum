namespace Spectrum.Application.Services
{
    public interface IEncryptionService
    {
        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="textToEncrypt">The text to encrypt.</param>
        /// <returns></returns>
        string EncryptString(string textToEncrypt);

        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="textToEncrypt">The text to encrypt.</param>
        /// <returns></returns>
        string EncryptString(int textToEncrypt);

        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="textToEncrypt">The text to encrypt.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string EncryptString(
            string textToEncrypt, 
            string key);

        /// <summary>
        /// Decrypts the string.
        /// </summary>
        /// <param name="textToDecrypt">The text to decrypt.</param>
        /// <returns></returns>
        string DecryptString(string textToDecrypt);

        /// <summary>
        /// Decrypts the string.
        /// </summary>
        /// <param name="textToDecrypt">The text to decrypt.</param>
        /// <param name="key">The ket.</param>
        /// <returns></returns>
        string DecryptString(
            string textToDecrypt,
            string key);
    }
}
