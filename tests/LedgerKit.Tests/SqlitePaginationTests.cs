using System;
using LedgerKit;
using Xunit;

namespace LedgerKit.Tests
{
    public class SqlitePaginationTests
    {
        [Fact]
        public void Pagination_Respects_Limit_And_Offset()
        {
            var path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".db");
            try
            {
                var cash = new Account("Cash", AccountType.Asset);
                var equity = new Account("Equity", AccountType.Equity);
                using (var store = new SqliteStore($"Data Source={path}"))
                {
                    store.AddAccount(cash);
                    store.AddAccount(equity);
                    for (var i = 0; i < 5; i++)
                        store.AddTransaction(new Transaction(DateTime.UtcNow.AddMinutes(i), "t" + i,
                            new[]
                            {
                                new Entry(cash, new Money(1m, Currency.Usd)),
                                new Entry(equity, new Money(-1m, Currency.Usd)),
                            }));
                    var page = store.GetTransactions(2, 1);
                    Assert.Equal(2, page.Count);
                }
            }
            finally
            {
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            }
        }
        // MARKER: pagination-tests
    }
}
