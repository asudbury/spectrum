namespace Spectrum.Content.Payments.Providers
{
    using Models;
    using System;
    using System.Collections.Generic;
    using Umbraco.Core;
    using Umbraco.Core.Persistence;

    public class DatabaseProvider : IDatabaseProvider
    {
        /// <inheritdoc />
        /// <summary>
        /// Inserts the transaction.
        /// </summary>
        /// <param name="model">The model.</param>
        public void InsertTransaction(TransactionModel model)
        {
            //// catch client id not set - might occur if code is regressed!
            if (model.ClientId == 0)
            {
                throw new ApplicationException("Insert Transaction - Client Id not set");
            }

            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            context.Database.Insert(model);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the transactions.
        /// </summary>
        /// <param name="dateRangeStart">The date range start.</param>
        /// <param name="dateRangeEnd">The date range end.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public IEnumerable<TransactionModel> GetTransactions(
            DateTime dateRangeStart, 
            DateTime dateRangeEnd, 
            int customerId)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            Sql sql = new Sql()
                .Select("*")
                .From(Content.Constants.Database.TransactionsTableName)
                .Where("CustomerId=" + customerId)
                .OrderByDescending("CreatedTime");

            return context.Database.Fetch<TransactionModel>(sql);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the payment.
        /// </summary>
        /// <param name="paymentId">The appointment identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public TransactionModel GetTransaction(
            string paymentId, 
            int customerId)
        {
            DatabaseContext context = ApplicationContext.Current.DatabaseContext;

            Sql sql = new Sql()
                .Select("*")
                .From(Content.Constants.Database.TransactionsTableName)
                .Where("TransactionId = '" + paymentId + "' and CustomerId= " + customerId);

            TransactionModel model = context.Database.FirstOrDefault<TransactionModel>(sql);

            return model;
        }
    }
}