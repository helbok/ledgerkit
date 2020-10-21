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
        // MARKER: transaction-tests
    }
}
