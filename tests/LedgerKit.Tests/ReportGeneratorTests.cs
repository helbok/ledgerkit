using LedgerKit;
using System;
using Xunit;

namespace LedgerKit.Tests
{
    public class ReportGeneratorTests
    {
        [Fact]
        public void TrialBalance_Contains_Account_Names()
        {
            var cash = new Account("Cash", AccountType.Asset);
            var income = new Account("Income", AccountType.Revenue);
            var journal = new Journal();
            journal.Add(new Transaction(DateTime.UtcNow, "sale",
                new[]
                {
                    new Entry(cash, new Money(50m, Currency.Usd)),
                    new Entry(income, new Money(-50m, Currency.Usd)),
                }));
            var report = new ReportGenerator(journal).TrialBalance();
            Assert.Contains("Cash", report);
            Assert.Contains("Income", report);
        }

        [Fact]
        public void IncomeStatement_Report_Contains_Net_Income()
        {
            var cash = new Account("Cash", AccountType.Asset);
            var revenue = new Account("Revenue", AccountType.Revenue);
            var journal = new Journal();
            journal.Add(new Transaction(DateTime.UtcNow, "sale",
                new[]
                {
                    new Entry(cash, new Money(75m, Currency.Usd)),
                    new Entry(revenue, new Money(-75m, Currency.Usd)),
                }));
            var report = new ReportGenerator(journal).IncomeStatement();
            Assert.Contains("Net income: 75.00", report);
        }

        [Fact]
        public void Regression_6_reports()
        {
            Assert.Equal(3m, (new Money(1m, Currency.Usd) + new Money(2m, Currency.Usd)).Amount);
        }

        [Fact]
        public void Regression_18_reports()
        {
            var c = new Currency("gbp", 2);
            Assert.Equal(2, c.MinorUnit);
        }

        [Fact]
        public void Regression_30_reports()
        {
            var m = new Money(12.34m, Currency.Usd);
            Assert.Equal(12.34m, m.Amount);
        }
        // MARKER: report-tests
    }
}
