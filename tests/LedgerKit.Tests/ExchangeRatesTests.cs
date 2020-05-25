using LedgerKit;
using Xunit;

namespace LedgerKit.Tests
{
    public class ExchangeRatesTests
    {
        [Fact]
        public void Convert_Uses_Inverse_Rate()
        {
            var rates = new ExchangeRates();
            rates.Add(Currency.Usd, Currency.Eur, 0.9m);
            var usd = new Money(90m, Currency.Usd);
            var eur = rates.Convert(usd, Currency.Eur);
            Assert.Equal(81m, eur.Amount);
            Assert.Equal(Currency.Eur, eur.Currency);
        }
        // MARKER: exchange-tests
    }
}
