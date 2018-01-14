namespace Spectrum.Content.Payments.Providers
{
    using Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IDatabaseProvider
    {
        /// <summary>
        /// Inserts the transaction.
        /// </summary>
        /// <param name="model">The model.</param>
        void InsertTransaction(TransactionModel model);
        
        /// <summary>
        /// Gets the transactions.
        /// </summary>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        IEnumerable<TransactionModel> GetTransactions(
            DateTime dateRangeStart,
            DateTime dateRangeEnd,
            int customerId);

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <param name="paymentId">The payment identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        TransactionModel GetTransaction(
            string paymentId,
            int customerId);
    }
}