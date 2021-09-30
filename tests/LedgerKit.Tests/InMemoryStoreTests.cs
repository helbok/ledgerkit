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
        // MARKER: inmemory-tests
    }
}
