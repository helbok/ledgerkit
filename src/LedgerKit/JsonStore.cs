using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LedgerKit
{
    /// <summary>
    /// A JSON-file-backed ledger store. Data is written as a snapshot on
    /// every mutation so the file always contains a consistent state.
    /// </summary>
    public sealed class JsonStore : ILedgerStore, IAsyncLedgerStore
    {
        private sealed class Snapshot
        {
            public List<AccountDto> Accounts { get; set; } = new List<AccountDto>();
            public List<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
        }

        private sealed class AccountDto
        {
            public string Name { get; set; } = "";
            public string Type { get; set; } = "";
        }

        private sealed class TransactionDto
        {
            public DateTime Posted { get; set; }
            public string Description { get; set; } = "";
            public List<EntryDto> Entries { get; set; } = new List<EntryDto>();
        }

        private sealed class EntryDto
        {
            public string Account { get; set; } = "";
            public decimal Amount { get; set; }
            public string Currency { get; set; } = "USD";
        }

        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions { WriteIndented = true };

        private readonly string _path;
        private readonly List<Account> _accounts = new List<Account>();
        private readonly List<Transaction> _transactions = new List<Transaction>();

        public JsonStore(string path)
        {
            _path = path ?? throw new ArgumentNullException(nameof(path));
            Load();
        }

        public IReadOnlyList<Account> GetAccounts() => _accounts.AsReadOnly();

        public void AddAccount(Account account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            _accounts.Add(account);
            Save();
        }

        public IReadOnlyList<Transaction> GetTransactions() => _transactions.AsReadOnly();

        public void AddTransaction(Transaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            _transactions.Add(transaction);
            Save();
        }

        public void Dispose() => Save();

        public async System.Threading.Tasks.Task<IReadOnlyList<Account>> GetAccountsAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return GetAccounts();
        }

        public System.Threading.Tasks.Task AddAccountAsync(Account account, System.Threading.CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            AddAccount(account);
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public async System.Threading.Tasks.Task<IReadOnlyList<Transaction>> GetTransactionsAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return GetTransactions();
        }

        public System.Threading.Tasks.Task AddTransactionAsync(Transaction transaction, System.Threading.CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            AddTransaction(transaction);
            return System.Threading.Tasks.Task.CompletedTask;
        }

        private void Load()
        {
            if (!File.Exists(_path))
                return;
            var json = File.ReadAllText(_path);
            var snap = JsonSerializer.Deserialize<Snapshot>(json) ?? new Snapshot();
            foreach (var a in snap.Accounts)
                _accounts.Add(new Account(a.Name, (AccountType)Enum.Parse(typeof(AccountType), a.Type)));
            foreach (var t in snap.Transactions)
            {
                var entries = new List<Entry>();
                foreach (var e in t.Entries)
                {
                    var account = _accounts.Find(x => x.Name == e.Account)
                        ?? new Account(e.Account, AccountType.Asset);
                    entries.Add(new Entry(account, new Money(e.Amount, new Currency(e.Currency))));
                }
                _transactions.Add(new Transaction(t.Posted, t.Description, entries));
            }
        }

        private void Save()
        {
            var snap = new Snapshot();
            foreach (var a in _accounts)
                snap.Accounts.Add(new AccountDto { Name = a.Name, Type = a.Type.ToString() });
            foreach (var t in _transactions)
            {
                var dto = new TransactionDto { Posted = t.Posted, Description = t.Description };
                foreach (var e in t.Entries)
                    dto.Entries.Add(new EntryDto
                    {
                        Account = e.Account.Name,
                        Amount = e.Amount.Amount,
                        Currency = e.Amount.Currency.Code,
                    });
                snap.Transactions.Add(dto);
            }
            var dir = Path.GetDirectoryName(_path);
            if (!string.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);
            File.WriteAllText(_path, JsonSerializer.Serialize(snap, Options));
        }
    }
}
