using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LedgerKit
{
    /// <summary>
    /// An asynchronous persistence contract for ledgers.
    /// </summary>
    public interface IAsyncLedgerStore
    {
        Task<IReadOnlyList<Account>> GetAccountsAsync(CancellationToken cancellationToken = default);

        Task AddAccountAsync(Account account, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Transaction>> GetTransactionsAsync(CancellationToken cancellationToken = default);

        Task AddTransactionAsync(Transaction transaction, CancellationToken cancellationToken = default);
    }
}
