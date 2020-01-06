using System;
using LedgerKit;
using Xunit;

namespace LedgerKit.Tests
{
    public class SqliteStoreTests
    {
        [Fact]
        public void SqliteStore_RoundTrips_Accounts()
        {
            var path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".db");
            try
            {
                using (var store = new SqliteStore($"Data Source={path}"))
                {
                    store.AddAccount(new Account("Cash", AccountType.Asset));
                }
                using (var store = new SqliteStore($"Data Source={path}"))
                {
                    Assert.Single(store.GetAccounts());
                }
            }
            finally
            {
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            }
        }
        // MARKER: sqlite-tests
    }
}
