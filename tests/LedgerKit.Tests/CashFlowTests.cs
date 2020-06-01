using LedgerKit;
using System;
using Xunit;

namespace LedgerKit.Tests
{
    public class CashFlowTests
    {
        [Fact]
        public void Operating_Cash_Flow_Matches_Net_Income()
        {
            var cash = new Account("Cash", AccountType.Asset);
            var revenue = new Account("Revenue", AccountType.Revenue);
            var journal = new Journal();
            journal.Add(new Transaction(DateTime.UtcNow, "sale",
                new[]
                {
                    new Entry(cash, new Money(300m, Currency.Usd)),
                    new Entry(revenue, new Money(-300m, Currency.Usd)),
                }));
            var flow = new CashFlow(new Ledger(journal));
            Assert.Equal(300m, flow.OperatingCashFlow);
        }
        // MARKER: cashflow-tests
    }
}
