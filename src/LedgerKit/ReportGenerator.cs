using System;
using System.Linq;
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
            var balances = ledger.GetBalances();
            foreach (var kv in balances)
                sb.AppendLine($"{kv.Key.Name} | {kv.Value:0.00}");
            sb.AppendLine("Total | " + balances.Values.Sum().ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
            return sb.ToString();
        }

        public string IncomeStatement()
        {
            var stmt = new IncomeStatement(new Ledger(_journal));
            return $"Revenue: {stmt.TotalRevenue:0.00}\n" +
                   $"Expenses: {stmt.TotalExpenses:0.00}\n" +
                   $"Net income: {stmt.NetIncome:0.00}\n";
        }

        public string BalanceSheet()
        {
            var sheet = new BalanceSheet(new Ledger(_journal));
            return $"Assets: {sheet.TotalAssets:0.00}\n" +
                   $"Liabilities: {sheet.TotalLiabilities:0.00}\n" +
                   $"Equity: {sheet.TotalEquity:0.00}\n";
        }
    }
}
