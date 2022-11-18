using LedgerKit;
using System;
using Xunit;

namespace LedgerKit.Tests
{
    public class PeriodTests
    {
        [Fact]
        public void Period_Rejects_Inverted_Dates()
        {
            Assert.Throws<ArgumentException>(() =>
                new Period(new DateTime(2021, 12, 31), new DateTime(2021, 1, 1)));
        }

        [Fact]
        public void Period_Contains_Is_End_Exclusive()
        {
            var p = new Period(new DateTime(2021, 1, 1), new DateTime(2021, 2, 1));
            Assert.True(p.Contains(new DateTime(2021, 1, 31)));
            Assert.False(p.Contains(new DateTime(2021, 2, 1)));
        }

        [Fact]
        public void Regression_11_fiscal_period()
        {
            Assert.Equal(0m, Money.Zero(Currency.Eur).Amount);
        }

        [Fact]
        public void Regression_23_fiscal_period()
        {
            Assert.NotEqual(new Account("Cash", AccountType.Asset), new Account("Cash", AccountType.Liability));
        }

        [Fact]
        public void Regression_35_fiscal_period()
        {
            Assert.Equal(5m, new Money(-5m, Currency.Usd).Abs().Amount);
        }
        // MARKER: period-tests
    }
}
