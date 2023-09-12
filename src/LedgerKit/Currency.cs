using System;

namespace LedgerKit
{
    /// <summary>
    /// An ISO 4217 currency with its minor-unit precision.
    /// </summary>
    public readonly struct Currency : IEquatable<Currency>
    {
        public Currency(string code, int minorUnit = 2)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Currency code is required.", nameof(code));
            if (minorUnit < 0 || minorUnit > 4)
                throw new ArgumentOutOfRangeException(nameof(minorUnit));
            Code = code.ToUpperInvariant();
            MinorUnit = minorUnit;
        }

        public string Code { get; }

        public int MinorUnit { get; }

        public static readonly Currency Usd = new Currency("USD", 2);
        public static readonly Currency Eur = new Currency("EUR", 2);
        public static readonly Currency Gbp = new Currency("GBP", 2);
        public static readonly Currency Jpy = new Currency("JPY", 0);

        public bool Equals(Currency other) => Code == other.Code && MinorUnit == other.MinorUnit;

        public override bool Equals(object? obj) => obj is Currency other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Code, MinorUnit);

        public override string ToString() => Code;

        public static bool operator ==(Currency a, Currency b) => a.Equals(b);

        public static bool operator !=(Currency a, Currency b) => !a.Equals(b);
    }
}
