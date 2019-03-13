using System;

namespace LedgerKit
{
    /// <summary>
    /// One side of a journal transaction: a signed amount posted to an account.
    /// </summary>
    public readonly struct Entry : IEquatable<Entry>
    {
        public Entry(Account account, Money amount)
        {
            Account = account ?? throw new ArgumentNullException(nameof(account));
            Amount = amount;
        }

        public Account Account { get; }

        public Money Amount { get; }

        public bool Equals(Entry other) =>
            Equals(Account, other.Account) && Amount == other.Amount;

        public override bool Equals(object? obj) => obj is Entry other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Account, Amount);

        public override string ToString() => $"{Amount} -> {Account.Name}";
    }
}
