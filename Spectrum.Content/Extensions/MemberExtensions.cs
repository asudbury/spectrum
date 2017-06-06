namespace Spectrum.Content.Extensions
{
    using System;
    using Umbraco.Core.Models;

    public static class MemberExtensions
    {
        /// <summary>
        /// Sets the value if has property.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        public static void SetValueIfHasProperty(
            this IMember instance,
            string propertyName,
            object value)
        {
            try
            {
                if (instance.HasProperty(propertyName))
                {
                    instance.SetValue(propertyName, value);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
