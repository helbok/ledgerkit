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
        // MARKER: report-tests
    }
}
