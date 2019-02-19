using System;

namespace LedgerKit
{
    /// <summary>
    /// A named account in the chart of accounts.
    /// </summary>
    public sealed class Account : IEquatable<Account>
    {
        public Account(string name, AccountType type)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Account name is required.", nameof(name));
            Name = name.Trim();
            Type = type;
        }

        public string Name { get; }

        public AccountType Type { get; }

        public bool Equals(Account? other) =>
            other != null && Name == other.Name && Type == other.Type;

        public override bool Equals(object? obj) => obj is Account other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Name, Type);

        public override string ToString() => $"{Name} ({Type})";
    }
}
