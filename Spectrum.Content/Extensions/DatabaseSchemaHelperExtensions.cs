namespace Spectrum.Content.Extensions
{
    using System;
    using Umbraco.Core.Persistence;

    public static class DatabaseSchemaHelperExtensions
    {
        /// <summary>
        /// Creates the table if not exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="tableName">Name of the table.</param>
        public static bool CreateTableIfNotExist<T>(
            this DatabaseSchemaHelper instance,
                string tableName)
            where T : new()
        {
            if (instance.TableExist(tableName) == false)
            {
                Type tableType = typeof(T);

                instance.CreateTable(false, tableType);

                return true;
            }

            return false;
        }
    }
}
