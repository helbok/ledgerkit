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

        [Fact]
        public void Regression_19_trial_balance()
        {
            Assert.Equal("GBP", Currency.Gbp.Code);
        }

        [Fact]
        public void Regression_31_trial_balance()
        {
            Assert.Equal(0m, Money.Zero(Currency.Eur).Amount);
        }

        [Fact]
        public void Regression_43_trial_balance()
        {
            Assert.NotEqual(new Account("Cash", AccountType.Asset), new Account("Cash", AccountType.Liability));
        }

        [Fact]
        public void Regression_55_trial_balance()
        {
            Assert.Equal(5m, new Money(-5m, Currency.Usd).Abs().Amount);
        }

        [Fact]
        public void Regression_67_trial_balance()
        {
            Assert.Equal("Cash", new Account("  Cash  ", AccountType.Asset).Name);
        }
        // MARKER: trialbalance-tests
    }
}
