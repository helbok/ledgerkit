using System;

namespace LedgerKit
{
    /// <summary>
    /// A fiscal period with inclusive start and exclusive end.
    /// </summary>
    public readonly struct Period : IEquatable<Period>
    {
        public Period(DateTime start, DateTime end)
        {
            if (end <= start)
                throw new ArgumentException("Period end must be after start.", nameof(end));
            Start = start;
            End = end;
        }

        public DateTime Start { get; }

        public DateTime End { get; }

        public bool Contains(DateTime instant) => instant >= Start && instant < End;

        public bool Equals(Period other) => Start == other.Start && End == other.End;

        public override bool Equals(object? obj) => obj is Period other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Start, End);

        public override string ToString() => $"{Start:yyyy-MM-dd}..{End:yyyy-MM-dd}";
    }
}
