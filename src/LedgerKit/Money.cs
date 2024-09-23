using System;

namespace LedgerKit
{
    /// <summary>
    /// An immutable monetary amount bound to a currency.
    /// </summary>
    public readonly struct Money : IEquatable<Money>, IComparable<Money>
    {
        private static readonly int[] Factors = { 1, 10, 100, 1000, 10000 };

        public Money(decimal amount, Currency currency)
        {
            Currency = currency;
            Amount = Round(amount, currency.MinorUnit);
        }

        public decimal Amount { get; }

        public Currency Currency { get; }

        public static Money Zero(Currency currency) => new Money(0m, currency);

        public Money Abs() => new Money(Math.Abs(Amount), Currency);

        public static bool TryParse(string text, out Money money)
        {
            money = default;
            if (string.IsNullOrWhiteSpace(text))
                return false;
            var parts = text.Split(' ');
            if (parts.Length != 2)
                return false;
            if (!decimal.TryParse(parts[0], System.Globalization.NumberStyles.Number,
                    System.Globalization.CultureInfo.InvariantCulture, out var amount))
                return false;
            money = new Money(amount, new Currency(parts[1]));
            return true;
        }

        public static Money operator +(Money a, Money b) => Combine(a, b, a.Amount + b.Amount);

        public static Money operator -(Money a, Money b) => Combine(a, b, a.Amount - b.Amount);

        public static Money operator -(Money a) => new Money(-a.Amount, a.Currency);

        public static Money operator *(Money a, decimal factor) => new Money(a.Amount * factor, a.Currency);

        public bool Equals(Money other) => Amount == other.Amount && Currency == other.Currency;

        public int CompareTo(Money other)
        {
            EnsureSameCurrency(this, other);
            return Amount.CompareTo(other.Amount);
        }

        public override bool Equals(object? obj) => obj is Money other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Amount, Currency);

        public override string ToString() => $"{Amount:0.00} {Currency.Code}";

        public static bool operator ==(Money a, Money b) => a.Equals(b);

        public static bool operator !=(Money a, Money b) => !a.Equals(b);

        public static bool operator <(Money a, Money b) => a.CompareTo(b) < 0;

        public static bool operator >(Money a, Money b) => a.CompareTo(b) > 0;

        private static Money Combine(Money a, Money b, decimal amount)
        {
            EnsureSameCurrency(a, b);
            return new Money(amount, a.Currency);
        }

        private static void EnsureSameCurrency(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException(
                    $"Cannot combine amounts in different currencies ({a.Currency.Code} vs {b.Currency.Code}).");
        }

        private static decimal Round(decimal amount, int minorUnit)
        {
            if (minorUnit < 0 || minorUnit >= Factors.Length)
                throw new ArgumentOutOfRangeException(nameof(minorUnit));
            var factor = Factors[minorUnit];
            if (amount > decimal.MaxValue / factor || amount < decimal.MinValue / factor)
                throw new OverflowException("Amount is too large for its minor unit.");
            return decimal.Round(amount * factor, 0, MidpointRounding.AwayFromZero) / factor;
        }
    }
}
