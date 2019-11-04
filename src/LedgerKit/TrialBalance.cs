using System;
using System.Collections.Generic;
using System.Linq;

namespace LedgerKit
{
    /// <summary>
    /// A snapshot of every account's debit/credit position.
    /// </summary>
    public sealed class TrialBalance
    {
        public TrialBalance(Journal journal)
        {
            if (journal == null) throw new ArgumentNullException(nameof(journal));
            Rows = Build(journal);
        }

        public IReadOnlyList<Row> Rows { get; }

        public bool IsBalanced => Rows.Sum(r => r.Debit - r.Credit) == 0m;

        public sealed class Row
        {
            public Row(Account account, decimal debit, decimal credit)
            {
                Account = account;
                Debit = debit;
                Credit = credit;
            }

            public Account Account { get; }
            public decimal Debit { get; }
            public decimal Credit { get; }
        }

        private static List<Row> Build(Journal journal)
        {
            var rows = new List<Row>();
            var ledger = new Ledger(journal);
            foreach (var kv in ledger.GetBalances())
            {
                var amount = kv.Value;
                rows.Add(amount >= 0m
                    ? new Row(kv.Key, amount, 0m)
                    : new Row(kv.Key, 0m, -amount));
            }
            return rows.OrderBy(r => r.Account.Name).ToList();
        }
    }
}
