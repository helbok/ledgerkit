using System;
using System.Collections.Generic;
using System.Linq;

namespace LedgerKit
{
    /// <summary>
    /// Compares persisted store transactions against an in-memory journal.
    /// </summary>
    public sealed class Reconciliation
    {
        public sealed class Result
        {
            public int MissingFromJournal { get; set; }

            public int MissingFromStore { get; set; }

            public bool IsReconciled => MissingFromJournal == 0 && MissingFromStore == 0;
        }

        public Result Reconcile(Journal journal, ILedgerStore store)
        {
            if (journal == null) throw new ArgumentNullException(nameof(journal));
            if (store == null) throw new ArgumentNullException(nameof(store));
            var result = new Result();
            var journalKeys = new HashSet<DateTime>(journal.Transactions.Select(t => t.Posted));
            var storeKeys = new HashSet<DateTime>(store.GetTransactions().Select(t => t.Posted));
            result.MissingFromStore = journalKeys.Count(k => !storeKeys.Contains(k));
            result.MissingFromJournal = storeKeys.Count(k => !journalKeys.Contains(k));
            return result;
        }
    }
}
