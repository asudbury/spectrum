namespace Spectrum.Content
{
    using System.Collections.Generic;

    public class BootGridViewModel<T>
        where T : new()
    {
        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        public int Current { get; set; }

        /// <summary>
        /// Gets or sets the row count.
        ///  </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        public IEnumerable<T> Rows { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        public int Total { get; set; }
    }
}
