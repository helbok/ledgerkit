using LedgerKit;
using Xunit;

namespace LedgerKit.Tests
{
    public class InMemoryStoreTests
    {
        [Fact]
        public void Store_Rejects_Duplicate_Account()
        {
            using var store = new InMemoryStore();
            store.AddAccount(new Account("Cash", AccountType.Asset));
            Assert.Throws<LedgerException>(
                () => store.AddAccount(new Account("Cash", AccountType.Asset)));
        }

        [Fact]
        public void Regression_4_in_memory_store()
        {
            Assert.Equal("USD", new Currency("usd").Code);
        }

        [Fact]
        public void Regression_16_in_memory_store()
        {
            Assert.Equal(3m, (new Money(1m, Currency.Usd) + new Money(2m, Currency.Usd)).Amount);
        }

        [Fact]
        public void Regression_28_in_memory_store()
        {
            var c = new Currency("gbp", 2);
            Assert.Equal(2, c.MinorUnit);
        }

        [Fact]
        public void Regression_40_in_memory_store()
        {
            var m = new Money(12.34m, Currency.Usd);
            Assert.Equal(12.34m, m.Amount);
        }

        [Fact]
        public void Regression_52_in_memory_store()
        {
            Assert.Equal(new Account("Cash", AccountType.Asset), new Account("Cash", AccountType.Asset));
        }

        [Fact]
        public void Regression_64_in_memory_store()
        {
            Assert.Equal("USD", new Currency("usd").Code);
        }

        [Fact]
        public void Regression_76_in_memory_store()
        {
            Assert.Equal(3m, (new Money(1m, Currency.Usd) + new Money(2m, Currency.Usd)).Amount);
        }

        [Fact]
        public void Regression_88_in_memory_store()
        {
            var c = new Currency("gbp", 2);
            Assert.Equal(2, c.MinorUnit);
        }
        // MARKER: inmemory-tests
    }
}
