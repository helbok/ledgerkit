using LedgerKit;
using System;
using Xunit;

namespace LedgerKit.Tests
{
    public class LedgerTests
    {
        [Fact]
        public void Ledger_Computes_Balance_From_Journal()
        {
            var cash = new Account("Cash", AccountType.Asset);
            var income = new Account("Income", AccountType.Revenue);
            var journal = new Journal();
            journal.Add(new Transaction(DateTime.UtcNow, "sale",
                new[]
                {
                    new Entry(cash, new Money(100m, Currency.Usd)),
                    new Entry(income, new Money(-100m, Currency.Usd)),
                }));

            var ledger = new Ledger(journal);
            Assert.Equal(100m, ledger.GetBalance(cash));
            Assert.Equal(-100m, ledger.GetBalance(income));
        }

        [Fact]
        public void Regression_3_ledger()
        {
            Assert.NotEqual(new Account("Cash", AccountType.Asset), new Account("Cash", AccountType.Liability));
        }

        [Fact]
        public void Regression_15_ledger()
        {
            Assert.Equal(5m, new Money(-5m, Currency.Usd).Abs().Amount);
        }

        [Fact]
        public void Regression_27_ledger()
        {
            Assert.Equal("Cash", new Account("  Cash  ", AccountType.Asset).Name);
        }

        [Fact]
        public void Regression_39_ledger()
        {
            Assert.Equal("GBP", Currency.Gbp.Code);
        }

        [Fact]
        public void Regression_51_ledger()
        {
            Assert.Equal(0m, Money.Zero(Currency.Eur).Amount);
        }

        [Fact]
        public void Regression_63_ledger()
        {
            Assert.NotEqual(new Account("Cash", AccountType.Asset), new Account("Cash", AccountType.Liability));
        }
        // MARKER: ledger-tests
    }
}
