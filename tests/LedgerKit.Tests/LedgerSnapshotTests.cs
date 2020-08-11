using LedgerKit;
using System;
using Xunit;

namespace LedgerKit.Tests
{
    public class LedgerSnapshotTests
    {
        [Fact]
        public void Snapshot_Is_Isolated_From_Later_Mutation()
        {
            var cash = new Account("Cash", AccountType.Asset);
            var equity = new Account("Equity", AccountType.Equity);
            var journal = new Journal();
            journal.Add(new Transaction(DateTime.UtcNow, "seed",
                new[]
                {
                    new Entry(cash, new Money(100m, Currency.Usd)),
                    new Entry(equity, new Money(-100m, Currency.Usd)),
                }));
            var snapshot = new Ledger(journal).Capture();
            journal.Add(new Transaction(DateTime.UtcNow, "extra",
                new[]
                {
                    new Entry(cash, new Money(50m, Currency.Usd)),
                    new Entry(equity, new Money(-50m, Currency.Usd)),
                }));
            Assert.Equal(100m, snapshot[cash]);
        }
        // MARKER: snapshot-tests
    }
}
