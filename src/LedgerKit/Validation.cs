using System;

namespace LedgerKit
{
    /// <summary>
    /// Helpers for validating ledger invariants.
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// Ensures the debits and credits of a set of entries sum to zero.
        /// </summary>
        public static bool IsBalanced(System.Collections.Generic.IEnumerable<Entry> entries)
        {
            var sum = 0m;
            foreach (var e in entries)
                sum += e.Amount.Amount;
            return sum == 0m;
        }

        public static void Require(bool condition, string message)
        {
            if (!condition)
                throw new LedgerException(message);
        }
    }
}
