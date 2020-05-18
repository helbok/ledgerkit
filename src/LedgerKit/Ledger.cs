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

        public IReadOnlyList<Transaction> GetTransactionsFor(Account account)
        {
            var result = new List<Transaction>();
            foreach (var tx in _journal.Transactions)
            {
                foreach (var e in tx.Entries)
                {
                    if (e.Account.Equals(account))
                    {
                        result.Add(tx);
                        break;
                    }
                }
            }
            return result;
        }

        public IReadOnlyDictionary<Account, decimal> GetBalancesByType(AccountType type)
        {
            return GetBalances()
                .Where(kv => kv.Key.Type == type)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}
