using LedgerKit;
using System.Threading.Tasks;
using Xunit;

namespace LedgerKit.Tests
{
    public class AsyncStoreTests
    {
        [Fact]
        public async Task InMemoryStore_Adds_Accounts_Async()
        {
            using var store = new InMemoryStore();
            await store.AddAccountAsync(new Account("Cash", AccountType.Asset));
            var accounts = await store.GetAccountsAsync();
            Assert.Single(accounts);
        }
        // MARKER: async-tests
    }
}
