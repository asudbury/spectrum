namespace Spectrum.Content.Payments.ViewModels
{
    using System.Collections.Generic;

    public class TransactionsViewModel
    {
        /// <summary>
        /// Gets the transactions.
        /// </summary>
        public IEnumerable<TransactionViewModel> Transactions { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionsViewModel"/> class.
        /// </summary>
        /// <param name="transactions">The transactions.</param>
        public TransactionsViewModel(IEnumerable<TransactionViewModel> transactions)
        {
            this.Transactions = transactions;
        }
    }
}
