namespace Spectrum.Content
{
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
    }
}
