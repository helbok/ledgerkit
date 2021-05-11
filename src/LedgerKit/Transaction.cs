using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LedgerKit
{
    /// <summary>
    /// A balanced double-entry transaction: every transaction is a list of
    /// entries whose signed amounts sum to zero.
    /// </summary>
    public sealed class Transaction
    {
        public Transaction(DateTime posted, string description, IEnumerable<Entry> entries)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.", nameof(description));
            var list = entries?.ToList() ?? throw new ArgumentNullException(nameof(entries));
            if (list.Count < 2)
                throw new LedgerException("A transaction requires at least two entries.");
            Validation.RequireBalanced(list, description);
            Posted = posted;
            Description = description.Trim();
            Entries = new ReadOnlyCollection<Entry>(list);
        }

        public DateTime Posted { get; }

        public string Description { get; }

        public IReadOnlyList<Entry> Entries { get; }
    }
}
