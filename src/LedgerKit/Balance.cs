namespace LedgerKit
{
    /// <summary>
    /// An account balance in a specific currency.
    /// </summary>
    public readonly struct Balance
    {
        public Balance(Account account, Money amount)
        {
            Account = account;
            Amount = amount;
        }

        public Account Account { get; }

        public Money Amount { get; }

        public override string ToString() => $"{Account.Name}: {Amount}";
    }
}
