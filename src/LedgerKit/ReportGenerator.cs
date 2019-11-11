using System;
using System.Text;

namespace LedgerKit
{
    /// <summary>
    /// Produces text reports from a journal.
    /// </summary>
    public sealed class ReportGenerator
    {
        private readonly Journal _journal;

        public ReportGenerator(Journal journal)
        {
            _journal = journal ?? throw new ArgumentNullException(nameof(journal));
        }

        public string TrialBalance()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Account | Balance");
            sb.AppendLine("--------|--------");
            var ledger = new Ledger(_journal);
            foreach (var kv in ledger.GetBalances())
                sb.AppendLine($"{kv.Key.Name} | {kv.Value:0.00}");
            sb.AppendLine("Total | " + ledger.GetBalances().Values.Sum().ToString("0.00"));
            return sb.ToString();
        }
    }
}
