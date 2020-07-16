using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LedgerKit
{
    /// <summary>
    /// An immutable snapshot of account balances at a point in time.
    /// </summary>
    public sealed class LedgerSnapshot
    {
        private readonly IReadOnlyDictionary<Account, decimal> _balances;

        public LedgerSnapshot(IReadOnlyDictionary<Account, decimal> balances)
        {
            if (balances == null) throw new ArgumentNullException(nameof(balances));
            _balances = new ReadOnlyDictionary<Account, decimal>(
                new Dictionary<Account, decimal>(balances));
        }

        public IReadOnlyDictionary<Account, decimal> Balances => _balances;

        public decimal this[Account account] =>
            _balances.TryGetValue(account, out var value) ? value : 0m;
    }
}
