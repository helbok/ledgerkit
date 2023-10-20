using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LedgerKit
{
    /// <summary>
    /// An in-memory ledger store, useful for tests and small applications.
    /// </summary>
    public sealed class InMemoryStore : ILedgerStore, IAsyncLedgerStore
    {
        private readonly List<Account> _accounts = new List<Account>();
        private readonly List<Transaction> _transactions = new List<Transaction>();

        public IReadOnlyList<Account> GetAccounts() => _accounts.ToList();

        public void AddAccount(Account account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            if (_accounts.Any(a => a.Equals(account)))
                throw new LedgerException($"Account '{account.Name}' already exists.");
            _accounts.Add(account);
        }

        public IReadOnlyList<Transaction> GetTransactions() => _transactions.ToList();

        public void AddTransaction(Transaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (_transactions.Contains(transaction))
                throw new LedgerException("Transaction already recorded.");
            _transactions.Add(transaction);
        }

        public void Dispose() { }

        public Task<IReadOnlyList<Account>> GetAccountsAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult(GetAccounts());

        public Task AddAccountAsync(Account account, CancellationToken cancellationToken = default)
        {
            AddAccount(account);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<Transaction>> GetTransactionsAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult(GetTransactions());

        public Task AddTransactionAsync(Transaction transaction, CancellationToken cancellationToken = default)
        {
            AddTransaction(transaction);
            return Task.CompletedTask;
        }
    }
}
