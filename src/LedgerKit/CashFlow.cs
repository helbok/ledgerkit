using System.Text;

namespace LedgerKit
{
    /// <summary>
    /// A simple indirect cash-flow statement builder.
    /// </summary>
    public sealed class CashFlow
    {
        private readonly Ledger _ledger;

        public CashFlow(Ledger ledger)
        {
            _ledger = ledger ?? throw new System.ArgumentNullException(nameof(ledger));
        }

        public decimal OperatingCashFlow => new IncomeStatement(_ledger).NetIncome;

        public string Render()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Cash flow statement");
            sb.AppendLine("-------------------");
            sb.AppendLine($"Operating cash flow: {OperatingCashFlow:0.00}");
            return sb.ToString();
        }
    }
}
