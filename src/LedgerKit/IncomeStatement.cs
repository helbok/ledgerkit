using System.Linq;

namespace LedgerKit
{
    /// <summary>
    /// A profit-and-loss statement computed from a ledger.
    /// </summary>
    public sealed class IncomeStatement
    {
        private readonly Ledger _ledger;

        public IncomeStatement(Ledger ledger)
        {
            _ledger = ledger ?? throw new System.ArgumentNullException(nameof(ledger));
        }

        /// <summary>Revenue has credit (negative) balances, so negate the sum.</summary>
        public decimal TotalRevenue => -_ledger.GetBalancesByType(AccountType.Revenue).Values.Sum();

        /// <summary>Expenses have debit (positive) balances.</summary>
        public decimal TotalExpenses => _ledger.GetBalancesByType(AccountType.Expense).Values.Sum();

        public decimal NetIncome => TotalRevenue - TotalExpenses;
    }
}
