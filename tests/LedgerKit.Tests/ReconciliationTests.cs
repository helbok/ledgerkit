using LedgerKit;
using System;
using Xunit;

namespace LedgerKit.Tests
{
    public class ReconciliationTests
    {
        [Fact]
        public void Reconciled_Store_And_Journal_Report_Clean()
        {
            var cash = new Account("Cash", AccountType.Asset);
            var equity = new Account("Equity", AccountType.Equity);
            var journal = new Journal();
            var tx = new Transaction(DateTime.UtcNow, "seed",
                new[]
                {
                    new Entry(cash, new Money(100m, Currency.Usd)),
                    new Entry(equity, new Money(-100m, Currency.Usd)),
                });
            journal.Add(tx);
            using var store = new InMemoryStore();
            store.AddTransaction(tx);
            var result = new Reconciliation().Reconcile(journal, store);
            Assert.True(result.IsReconciled);
        }
        // MARKER: reconciliation-tests
    }
}
