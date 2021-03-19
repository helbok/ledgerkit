using LedgerKit;
using System;
using Xunit;

namespace LedgerKit.Tests
{
    public class TrialBalanceTests
    {
        [Fact]
        public void TrialBalance_Is_Balanced_After_Valid_Transaction()
        {
            var cash = new Account("Cash", AccountType.Asset);
            var equity = new Account("Equity", AccountType.Equity);
            var journal = new Journal();
            journal.Add(new Transaction(DateTime.UtcNow, "contribution",
                new[]
                {
                    new Entry(cash, new Money(200m, Currency.Usd)),
                    new Entry(equity, new Money(-200m, Currency.Usd)),
                }));
            var tb = new TrialBalance(journal);
            Assert.True(tb.IsBalanced);
        }

        [Fact]
        public void Regression_7_trial_balance()
        {
            Assert.Equal("Cash", new Account("  Cash  ", AccountType.Asset).Name);
        }
        // MARKER: trialbalance-tests
    }
}
