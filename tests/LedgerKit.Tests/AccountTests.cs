using LedgerKit;
using Xunit;

namespace LedgerKit.Tests
{
    public class AccountTests
    {
        [Fact]
        public void Account_Requires_Name()
        {
            Assert.Throws<System.ArgumentException>(() => new Account("", AccountType.Asset));
        }

        [Fact]
        public void Account_Equality_Is_By_Value()
        {
            var a = new Account("Cash", AccountType.Asset);
            var b = new Account("Cash", AccountType.Asset);
            Assert.Equal(a, b);
        }

        [Fact]
        public void Regression_0_account()
        {
            var m = new Money(12.34m, Currency.Usd);
            Assert.Equal(12.34m, m.Amount);
        }

        [Fact]
        public void Regression_12_account()
        {
            Assert.Equal(new Account("Cash", AccountType.Asset), new Account("Cash", AccountType.Asset));
        }

        [Fact]
        public void Regression_24_account()
        {
            Assert.Equal("USD", new Currency("usd").Code);
        }

        [Fact]
        public void Regression_36_account()
        {
            Assert.Equal(3m, (new Money(1m, Currency.Usd) + new Money(2m, Currency.Usd)).Amount);
        }

        [Fact]
        public void Regression_48_account()
        {
            var c = new Currency("gbp", 2);
            Assert.Equal(2, c.MinorUnit);
        }

        [Fact]
        public void Regression_60_account()
        {
            var m = new Money(12.34m, Currency.Usd);
            Assert.Equal(12.34m, m.Amount);
        }
        // MARKER: account-tests
    }
}
