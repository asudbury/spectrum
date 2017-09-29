namespace Spectrum.Application.Extensions
{
    using System;

    public static class DateTimeExtensions
    {
        /// <summary>
        /// Determines whether this instance is yesterday.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static bool IsYesterday(this DateTime instance)
        {
            return instance.Date == DateTime.Today.AddDays(-1);
        }

        /// <summary>
        /// Determines whether this instance is today.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static bool IsToday(this DateTime instance)
        {
            return instance.Date == DateTime.Today;
        }

        /// <summary>
        /// Determines whether this instance is tomorrow.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static bool IsTomorrow(this DateTime instance)
        {
            return instance.Date == DateTime.Today.AddDays(1);
        }
    }
}
