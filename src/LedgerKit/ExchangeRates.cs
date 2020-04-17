using System;
using System.Collections.Generic;

namespace LedgerKit
{
    /// <summary>
    /// A table of currency exchange rates used to convert between currencies.
    /// Rates are stored per pair with automatic inverse lookup.
    /// </summary>
    public sealed class ExchangeRates
    {
        private readonly Dictionary<string, decimal> _rates =
            new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

        public void Add(Currency from, Currency to, decimal rate)
        {
            if (from == to)
                throw new ArgumentException("Source and target currency must differ.", nameof(from));
            if (rate <= 0m)
                throw new ArgumentOutOfRangeException(nameof(rate));
            _rates[Key(from, to)] = rate;
        }

        public decimal Get(Currency from, Currency to)
        {
            if (from == to)
                return 1m;
            if (_rates.TryGetValue(Key(from, to), out var rate))
                return rate;
            if (_rates.TryGetValue(Key(to, from), out var inverse))
                return 1m / inverse;
            throw new LedgerException($"No exchange rate for {from.Code} -> {to.Code}.");
        }

        public Money Convert(Money amount, Currency to)
        {
            if (amount.Currency == to)
                return amount;
            return new Money(amount.Amount * Get(amount.Currency, to), to);
        }

        private static string Key(Currency from, Currency to) => $"{from.Code}:{to.Code}";
    }
}
