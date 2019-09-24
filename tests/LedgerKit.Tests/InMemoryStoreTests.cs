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
        // MARKER: inmemory-tests
    }
}
