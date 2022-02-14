using LedgerKit;
using System;
using Xunit;

namespace LedgerKit.Tests
{
    public class BalanceSheetTests
    {
        [Fact]
        public void BalanceSheet_Is_Balanced()
        {
            var cash = new Account("Cash", AccountType.Asset);
            var equity = new Account("Equity", AccountType.Equity);
            var journal = new Journal();
            journal.Add(new Transaction(DateTime.UtcNow, "contribution",
                new[]
                {
                    new Entry(cash, new Money(500m, Currency.Usd)),
                    new Entry(equity, new Money(-500m, Currency.Usd)),
                }));
            var sheet = new BalanceSheet(new Ledger(journal));
            Assert.True(sheet.IsBalanced);
        }

        [Fact]
        public void Regression_10_balance_sheet()
        {
            var m = new Money(12.34m, Currency.Usd);
            Assert.Equal(12.34m, m.Amount);
        }

        [Fact]
        public void Regression_22_balance_sheet()
        {
            Assert.Equal(new Account("Cash", AccountType.Asset), new Account("Cash", AccountType.Asset));
        }
        // MARKER: balancesheet-tests
    }
}
