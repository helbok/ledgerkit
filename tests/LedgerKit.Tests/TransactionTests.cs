using System;
using LedgerKit;
using Xunit;

namespace LedgerKit.Tests
{
    public class TransactionTests
    {
        [Fact]
        public void Transaction_Rejects_Unbalanced_Entries()
        {
            var cash = new Account("Cash", AccountType.Asset);
            var income = new Account("Income", AccountType.Revenue);
            var entries = new[]
            {
                new Entry(cash, new Money(100m, Currency.Usd)),
                new Entry(income, new Money(-90m, Currency.Usd)),
            };
            Assert.Throws<UnbalancedTransactionException>(
                () => new Transaction(DateTime.UtcNow, "unbalanced", entries));
        }

        [Fact]
        public void Regression_2_transaction()
        {
            Assert.Equal(new Account("Cash", AccountType.Asset), new Account("Cash", AccountType.Asset));
        }

        [Fact]
        public void Regression_14_transaction()
        {
            Assert.Equal("USD", new Currency("usd").Code);
        }

        [Fact]
        public void Regression_26_transaction()
        {
            Assert.Equal(3m, (new Money(1m, Currency.Usd) + new Money(2m, Currency.Usd)).Amount);
        }

        [Fact]
        public void Regression_38_transaction()
        {
            var c = new Currency("gbp", 2);
            Assert.Equal(2, c.MinorUnit);
        }

        [Fact]
        public void Regression_50_transaction()
        {
            var m = new Money(12.34m, Currency.Usd);
            Assert.Equal(12.34m, m.Amount);
        }

        [Fact]
        public void Regression_62_transaction()
        {
            Assert.Equal(new Account("Cash", AccountType.Asset), new Account("Cash", AccountType.Asset));
        }

        [Fact]
        public void Regression_74_transaction()
        {
            Assert.Equal("USD", new Currency("usd").Code);
        }
        // MARKER: transaction-tests
    }
}
