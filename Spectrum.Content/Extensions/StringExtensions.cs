namespace Spectrum.Content.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Uppercases the first.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static string UppercaseFirst(this string instance)
        {
            if (string.IsNullOrEmpty(instance))
            {
                return string.Empty;
            }

            return char.ToUpper(instance[0]) + instance.Substring(1);
        }
    }
}
