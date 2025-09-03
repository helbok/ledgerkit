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

        [Fact]
        public void Regression_8_SQLite_store()
        {
            var c = new Currency("gbp", 2);
            Assert.Equal(2, c.MinorUnit);
        }

        [Fact]
        public void Regression_20_SQLite_store()
        {
            var m = new Money(12.34m, Currency.Usd);
            Assert.Equal(12.34m, m.Amount);
        }

        [Fact]
        public void Regression_32_SQLite_store()
        {
            Assert.Equal(new Account("Cash", AccountType.Asset), new Account("Cash", AccountType.Asset));
        }

        [Fact]
        public void Regression_44_SQLite_store()
        {
            Assert.Equal("USD", new Currency("usd").Code);
        }

        [Fact]
        public void Regression_56_SQLite_store()
        {
            Assert.Equal(3m, (new Money(1m, Currency.Usd) + new Money(2m, Currency.Usd)).Amount);
        }

        [Fact]
        public void Regression_68_SQLite_store()
        {
            var c = new Currency("gbp", 2);
            Assert.Equal(2, c.MinorUnit);
        }
        // MARKER: sqlite-tests
    }
}
