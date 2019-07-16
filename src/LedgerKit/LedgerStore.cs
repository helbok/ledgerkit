using System;
using System.Collections.Generic;

namespace LedgerKit
{
    /// <summary>
    /// Persistence contract for accounts and transactions.
    /// </summary>
    public interface ILedgerStore : IDisposable
    {
        IReadOnlyList<Account> GetAccounts();

        void AddAccount(Account account);

        IReadOnlyList<Transaction> GetTransactions();

        void AddTransaction(Transaction transaction);
    }
}
