using System.Linq;

namespace LedgerKit
{
    /// <summary>
    /// A statement of financial position computed from a ledger.
    /// </summary>
    public sealed class BalanceSheet
    {
        private readonly Ledger _ledger;

        public BalanceSheet(Ledger ledger)
        {
            _ledger = ledger ?? throw new System.ArgumentNullException(nameof(ledger));
        }

        public decimal TotalAssets => _ledger.GetBalancesByType(AccountType.Asset).Values.Sum();

        public decimal TotalLiabilities => -_ledger.GetBalancesByType(AccountType.Liability).Values.Sum();

        public decimal TotalEquity => -_ledger.GetBalancesByType(AccountType.Equity).Values.Sum();

        public bool IsBalanced => TotalAssets == TotalLiabilities + TotalEquity;
    }
}
