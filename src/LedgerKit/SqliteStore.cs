using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace LedgerKit
{
    /// <summary>
    /// A SQLite-backed ledger store. Accounts and entries are persisted in a
    /// normalized schema with a transaction log.
    /// </summary>
    public sealed class SqliteStore : ILedgerStore, IAsyncLedgerStore
    {
        private readonly SqliteConnection _connection;

        public SqliteStore(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string is required.", nameof(connectionString));
            _connection = new SqliteConnection(connectionString);
            _connection.Open();
            InitializeSchema();
        }

        public IReadOnlyList<Account> GetAccounts()
        {
            var list = new List<Account>();
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT name, type FROM accounts ORDER BY name;";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(new Account(reader.GetString(0),
                    (AccountType)Enum.Parse(typeof(AccountType), reader.GetString(1))));
            return list;
        }

        public void AddAccount(Account account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = "INSERT INTO accounts (name, type) VALUES ($name, $type);";
            cmd.Parameters.AddWithValue("$name", account.Name);
            cmd.Parameters.AddWithValue("$type", account.Type.ToString());
            cmd.ExecuteNonQuery();
        }

        public IReadOnlyList<Transaction> GetTransactions()
        {
            var list = new List<Transaction>();
            var accountsByName = new Dictionary<string, Account>();
            foreach (var a in GetAccounts())
                accountsByName[a.Name] = a;
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = @"
                SELECT t.id, t.posted, t.description,
                       e.account_name, e.amount, e.currency
                FROM transactions t
                JOIN entries e ON e.transaction_id = t.id
                ORDER BY t.posted, t.id;";
            using var reader = cmd.ExecuteReader();
            long? currentId = null;
            DateTime posted = default;
            string description = "";
            var entries = new List<Entry>();
            while (reader.Read())
            {
                var id = reader.GetInt64(0);
                if (currentId != id)
                {
                    if (currentId != null)
                        list.Add(new Transaction(posted, description, entries));
                    currentId = id;
                    posted = DateTime.Parse(reader.GetString(1), System.Globalization.CultureInfo.InvariantCulture);
                    description = reader.GetString(2);
                    entries = new List<Entry>();
                }
                var account = accountsByName[reader.GetString(3)];
                entries.Add(new Entry(account, new Money(reader.GetDecimal(4), new Currency(reader.GetString(5)))));
            }
            if (currentId != null)
                list.Add(new Transaction(posted, description, entries));
            return list;
        }

        public void AddTransaction(Transaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            using var tx = _connection.BeginTransaction();
            using (var cmd = _connection.CreateCommand())
            {
                cmd.Transaction = tx;
                cmd.CommandText = "INSERT INTO transactions (posted, description) VALUES ($posted, $description); SELECT last_insert_rowid();";
                cmd.Parameters.AddWithValue("$posted", transaction.Posted);
                cmd.Parameters.AddWithValue("$description", transaction.Description);
                var id = (long)cmd.ExecuteScalar();

                foreach (var entry in transaction.Entries)
                {
                    var ec = _connection.CreateCommand();
                    ec.Transaction = tx;
                    ec.CommandText = "INSERT INTO entries (transaction_id, account_name, amount, currency) VALUES ($tid, $account, $amount, $currency);";
                    ec.Parameters.AddWithValue("$tid", id);
                    ec.Parameters.AddWithValue("$account", entry.Account.Name);
                    ec.Parameters.AddWithValue("$amount", entry.Amount.Amount);
                    ec.Parameters.AddWithValue("$currency", entry.Amount.Currency.Code);
                    ec.ExecuteNonQuery();
                }
            }
            tx.Commit();
        }

        public void Dispose() => _connection.Dispose();

        public System.Threading.Tasks.Task<IReadOnlyList<Account>> GetAccountsAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return System.Threading.Tasks.Task.FromResult(GetAccounts());
        }

        public System.Threading.Tasks.Task AddAccountAsync(Account account, System.Threading.CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            AddAccount(account);
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task<IReadOnlyList<Transaction>> GetTransactionsAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return System.Threading.Tasks.Task.FromResult(GetTransactions());
        }

        public System.Threading.Tasks.Task AddTransactionAsync(Transaction transaction, System.Threading.CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            AddTransaction(transaction);
            return System.Threading.Tasks.Task.CompletedTask;
        }

        private void InitializeSchema()
        {
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS accounts (
                    name TEXT PRIMARY KEY,
                    type TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS transactions (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    posted TEXT NOT NULL,
                    description TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS entries (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    transaction_id INTEGER NOT NULL,
                    account_name TEXT NOT NULL,
                    amount REAL NOT NULL,
                    currency TEXT NOT NULL
                );
                CREATE INDEX IF NOT EXISTS idx_entries_transaction ON entries(transaction_id);";
            cmd.ExecuteNonQuery();
        }
    }
}
