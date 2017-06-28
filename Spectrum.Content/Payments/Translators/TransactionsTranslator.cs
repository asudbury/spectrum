namespace Spectrum.Content.Payments.Translators
{
    using System.Collections.Generic;
    using Braintree;
    using ViewModels;

    public class TransactionsTranslator : ITransactionsTranslator
    {
        /// <summary>
        /// The transaction translator.
        /// </summary>
        private readonly ITransactionTranslator transactionTranslator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionsTranslator"/> class.
        /// </summary>
        /// <param name="transactionTranslator">The transaction translator.</param>
        public TransactionsTranslator(ITransactionTranslator transactionTranslator)
        {
            this.transactionTranslator = transactionTranslator;
        }

        /// <summary>
        /// Translates the specified transactions.
        /// </summary>
        /// <param name="transactions">The transactions.</param>
        /// <returns></returns>
        public TransactionsViewModel Translate(ResourceCollection<Transaction> transactions)
        {
            List<TransactionViewModel> transactionViewModelList = new List<TransactionViewModel>();

            foreach (Transaction transaction in transactions)
            { 
                transactionViewModelList.Add(transactionTranslator.Translate(transaction));
            }

            return new TransactionsViewModel(transactionViewModelList);
        }
    }
}
