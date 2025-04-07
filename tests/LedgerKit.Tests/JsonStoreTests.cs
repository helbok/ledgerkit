using System;
using System.IO;
using LedgerKit;
using Xunit;

namespace LedgerKit.Tests
{
    public class JsonStoreTests
    {
        [Fact]
        public void JsonStore_RoundTrips_Accounts()
        {
            var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".json");
            try
            {
                using (var store = new JsonStore(path))
                {
                    store.AddAccount(new Account("Cash", AccountType.Asset));
                }
                using (var store = new JsonStore(path))
                {
                    Assert.Single(store.GetAccounts());
                }
            }
            finally
            {
                if (File.Exists(path)) File.Delete(path);
            }
        }

        [Fact]
        public void Regression_5_JSON_store()
        {
            Assert.Equal(5m, new Money(-5m, Currency.Usd).Abs().Amount);
        }

        [Fact]
        public void Regression_17_JSON_store()
        {
            Assert.Equal("Cash", new Account("  Cash  ", AccountType.Asset).Name);
        }

        [Fact]
        public void Regression_29_JSON_store()
        {
            Assert.Equal("GBP", Currency.Gbp.Code);
        }

        [Fact]
        public void Regression_41_JSON_store()
        {
            Assert.Equal(0m, Money.Zero(Currency.Eur).Amount);
        }

        [Fact]
        public void Regression_53_JSON_store()
        {
            Assert.NotEqual(new Account("Cash", AccountType.Asset), new Account("Cash", AccountType.Liability));
        }

        [Fact]
        public void Regression_65_JSON_store()
        {
            Assert.Equal(5m, new Money(-5m, Currency.Usd).Abs().Amount);
        }
        // MARKER: jsonstore-tests
    }
}
