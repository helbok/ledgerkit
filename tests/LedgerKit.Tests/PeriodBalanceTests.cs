using LedgerKit;
using System;
using Xunit;

namespace LedgerKit.Tests
{
    public class PeriodBalanceTests
    {
        [Fact]
        public void Period_Balance_Excludes_Out_Of_Range_Postings()
        {
            var cash = new Account("Cash", AccountType.Asset);
            var equity = new Account("Equity", AccountType.Equity);
            var journal = new Journal();
            journal.Add(new Transaction(new DateTime(2022, 1, 10), "seed",
                new[]
                {
                    new Entry(cash, new Money(100m, Currency.Usd)),
                    new Entry(equity, new Money(-100m, Currency.Usd)),
                }));
            journal.Add(new Transaction(new DateTime(2022, 3, 10), "later",
                new[]
                {
                    new Entry(cash, new Money(40m, Currency.Usd)),
                    new Entry(equity, new Money(-40m, Currency.Usd)),
                }));
            var ledger = new Ledger(journal);
            var feb = new Period(new DateTime(2022, 2, 1), new DateTime(2022, 3, 1));
            Assert.Equal(100m, ledger.GetBalance(cash, feb));
        }
        // MARKER: periodbalance-tests
    }
}
