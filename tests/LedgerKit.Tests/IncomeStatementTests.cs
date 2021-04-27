using LedgerKit;
using System;
using Xunit;

namespace LedgerKit.Tests
{
    public class IncomeStatementTests
    {
        [Fact]
        public void NetIncome_Equals_Revenue_Minus_Expenses()
        {
            var cash = new Account("Cash", AccountType.Asset);
            var revenue = new Account("Revenue", AccountType.Revenue);
            var expense = new Account("Rent", AccountType.Expense);
            var journal = new Journal();
            journal.Add(new Transaction(DateTime.UtcNow, "sale",
                new[]
                {
                    new Entry(cash, new Money(1000m, Currency.Usd)),
                    new Entry(revenue, new Money(-1000m, Currency.Usd)),
                }));
            journal.Add(new Transaction(DateTime.UtcNow, "rent",
                new[]
                {
                    new Entry(expense, new Money(400m, Currency.Usd)),
                    new Entry(cash, new Money(-400m, Currency.Usd)),
                }));

            var stmt = new IncomeStatement(new Ledger(journal));
            Assert.Equal(1000m, stmt.TotalRevenue);
            Assert.Equal(400m, stmt.TotalExpenses);
            Assert.Equal(600m, stmt.NetIncome);
        }

        [Fact]
        public void Regression_9_income_statement()
        {
            Assert.Equal("GBP", Currency.Gbp.Code);
        }
        // MARKER: income-tests
    }
}
