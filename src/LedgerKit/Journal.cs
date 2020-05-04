using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LedgerKit
{
    /// <summary>
    /// An append-only list of transactions.
    /// </summary>
    public sealed class Journal
    {
        private readonly List<Transaction> _transactions = new List<Transaction>();

        public void Add(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));
            _transactions.Add(transaction);
        }

        public IReadOnlyList<Transaction> Transactions =>
            new ReadOnlyCollection<Transaction>(_transactions);

        public int Count => _transactions.Count;

        public IReadOnlyList<Transaction> GetRange(Period period)
        {
            var result = new List<Transaction>();
            foreach (var tx in _transactions)
                if (period.Contains(tx.Posted))
                    result.Add(tx);
            return result;
        }
    }
}
