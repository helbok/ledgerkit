using System;

namespace LedgerKit
{
    /// <summary>
    /// Base exception for ledger validation failures.
    /// </summary>
    public class LedgerException : Exception
    {
        public LedgerException(string message) : base(message) { }

        public LedgerException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Thrown when a transaction does not balance.
    /// </summary>
    public sealed class UnbalancedTransactionException : LedgerException
    {
        public UnbalancedTransactionException(string message) : base(message) { }
    }

    /// <summary>
    /// Thrown when an account or transaction cannot be found.
    /// </summary>
    public sealed class LedgerNotFoundException : LedgerException
    {
        public LedgerNotFoundException(string message) : base(message) { }
    }
}
