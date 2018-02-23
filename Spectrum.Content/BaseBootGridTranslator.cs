namespace Spectrum.Content
{
    using Models;
    using Scorchio.ExtensionMethods;
    using System;

    public abstract class BaseBootGridTranslator
    {
        /// <summary>
        /// Gets the range.
        /// </summary>
        /// <param name="objectCount">The object count.</param>
        /// <param name="current">The current.</param>
        /// <param name="rowCount">The row count.</param>
        /// <returns></returns>
        protected Tuple<int, int> GetRange(
            int objectCount,
            int current,
            int rowCount)
        {
            int start = 0;
            int end = objectCount;

            if (objectCount == 0)
            {
                return new Tuple<int, int>(-1, -1);
            }

            if (rowCount == -1)
            {
                end = objectCount;
            }

            if (current > 1)
            {
                start = (current - 1) * rowCount;
            }

            if (objectCount < start * rowCount)
            {
                end = objectCount - start;
            }

            if (end > rowCount && rowCount != -1)
            {
                end = rowCount;
            }
            
            return new Tuple<int, int>(start, end);
        }

        /// <summary>
        /// Determines whether [is sort order ascending] [the specified sort type].
        /// </summary>
        /// <param name="sortType">Type of the sort.</param>
        protected bool IsSortOrderAscending(string sortType)
        {
            return sortType == SortOrder.asc.ToString();
        }

        /// <summary>
        /// Determines whether [is date check keyword search] [the specified search string].
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <param name="dateTime">The date time.</param>
        protected bool IsDateCheckKeywordSearch(
            string searchString,
            DateTime dateTime)
        {
            if (searchString.ToLower() == "yesterday" &&
                dateTime.IsYesterday())
            {
                return true;
            }

            if (searchString.ToLower() == "today" &&
                dateTime.IsToday())
            {
                return true;
            }

            if (searchString.ToLower() == "tomorrow" &&
                dateTime.IsTomorrow())
            {
                return true;
            }

            return false;
        }
    }
}
