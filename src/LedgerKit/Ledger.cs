using System;
using System.Collections.Generic;
using System.Linq;

namespace LedgerKit
{
    /// <summary>
    /// Computes per-account balances from a journal.
    /// </summary>
    public sealed class Ledger
    {
        private readonly Journal _journal;

        public Ledger(Journal journal)
        {
            _journal = journal ?? throw new ArgumentNullException(nameof(journal));
        }

        public decimal GetBalance(Account account)
        {
            decimal sum = 0m;
            foreach (var tx in _journal.Transactions)
            {
                foreach (var e in tx.Entries)
                {
                    if (e.Account.Equals(account))
                        sum += e.Amount.Amount;
                }
            }
            return sum;
        }

        public IReadOnlyDictionary<Account, decimal> GetBalances()
        {
            var map = new Dictionary<Account, decimal>();
            foreach (var tx in _journal.Transactions)
            {
                foreach (var e in tx.Entries)
                {
                    map.TryGetValue(e.Account, out var current);
                    map[e.Account] = current + e.Amount.Amount;
                }
            }
            return map;
        }
    }
}
