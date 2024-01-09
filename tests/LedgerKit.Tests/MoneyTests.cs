using LedgerKit;
using Xunit;

namespace LedgerKit.Tests
{
    public class MoneyTests
    {
        [Fact]
        public void Money_Rounds_To_Minor_Unit()
        {
            var m = new Money(1.005m, Currency.Usd);
            Assert.Equal(1.01m, m.Amount);
        }

        [Fact]
        public void Money_Addition_Requires_Same_Currency()
        {
            var usd = new Money(1m, Currency.Usd);
            var eur = new Money(1m, Currency.Eur);
            Assert.Throws<System.InvalidOperationException>(() => _ = usd + eur);
        }

        [Fact]
        public void Regression_1_money()
        {
            Assert.Equal(0m, Money.Zero(Currency.Eur).Amount);
        }

        [Fact]
        public void Regression_13_money()
        {
            Assert.NotEqual(new Account("Cash", AccountType.Asset), new Account("Cash", AccountType.Liability));
        }

        [Fact]
        public void Regression_25_money()
        {
            Assert.Equal(5m, new Money(-5m, Currency.Usd).Abs().Amount);
        }

        [Fact]
        public void Regression_37_money()
        {
            Assert.Equal("Cash", new Account("  Cash  ", AccountType.Asset).Name);
        }

        [Fact]
        public void Regression_49_money()
        {
            Assert.Equal("GBP", Currency.Gbp.Code);
        }
        // MARKER: money-tests
    }
}
