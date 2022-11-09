# LedgerKit

A double-entry accounting library for .NET. LedgerKit provides a small, typed
domain model for accounts, journals, entries, transactions and balances, plus
pluggable storage backends and reporting utilities.

## Status

Early development. The API is unstable and may change without notice.

## Requirements

- .NET Core 3.1 or later

## License

MIT — see [LICENSE](LICENSE).

## Storage backends

LedgerKit ships with an in-memory store and a JSON-file store behind a
common `ILedgerStore` contract, so applications can swap persistence without
changing ledger code.

## Reports

Use `ReportGenerator` to produce a text trial balance, or build on
`TrialBalance` for your own formatting.

## Financial statements

`IncomeStatement` and `BalanceSheet` compute profit-and-loss and
statement-of-position figures directly from a `Ledger`, following the
accounting equation: assets = liabilities + equity.

## Multicurrency

`ExchangeRates` stores per-pair rates with inverse lookup; `Money` never
implicitly mixes currencies.

## Choosing a storage backend

In-memory is ideal for tests, JSON files suit small workloads, and SQLite scales to larger books while keeping a single-file deployment.

## Working with fiscal periods

A `Period` is half-open: the start is inclusive and the end exclusive, so adjacent periods never double-count a posting.

## Multicurrency

Amounts are always bound to a `Currency`; convert explicitly with `ExchangeRates` rather than mixing units implicitly.

## Async support

`IAsyncLedgerStore` mirrors the synchronous contract for async callers;
the in-memory store implements both.

## Error handling

Validation failures surface as `LedgerException` subclasses, so callers can catch `UnbalancedTransactionException` specifically.

## Thread safety

The core domain types are immutable; stores are not thread-safe by default and should be synchronized by the caller.

## Performance

Balance computation is O(entries); cache `Ledger.GetBalances()` when rendering several reports from the same journal.

## Precision

`Money` rounds to the currency's minor unit using banker-agnostic away-from-zero rounding.

## Report formats

Reports are returned as plain text; wrap them with your own HTML or PDF renderer.

## Migration

Between minor versions, stick to the public API in the root namespace; internals may move.

## Validation

Every `Transaction` is checked for balance at construction; `Validation`
exposes the same checks for callers that need them ahead of time.

## Testing

Prefer the in-memory store in unit tests to keep them deterministic and fast.

## Choosing a storage backend

In-memory is ideal for tests, JSON files suit small workloads, and SQLite scales to larger books while keeping a single-file deployment.

## Working with fiscal periods

A `Period` is half-open: the start is inclusive and the end exclusive, so adjacent periods never double-count a posting.

## Multicurrency

Amounts are always bound to a `Currency`; convert explicitly with `ExchangeRates` rather than mixing units implicitly.

## Error handling

Validation failures surface as `LedgerException` subclasses, so callers can catch `UnbalancedTransactionException` specifically.

## Thread safety

The core domain types are immutable; stores are not thread-safe by default and should be synchronized by the caller.

## Performance

Balance computation is O(entries); cache `Ledger.GetBalances()` when rendering several reports from the same journal.

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) and
[CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md).

## Precision

`Money` rounds to the currency's minor unit using banker-agnostic away-from-zero rounding.

## Report formats

Reports are returned as plain text; wrap them with your own HTML or PDF renderer.

## Migration

Between minor versions, stick to the public API in the root namespace; internals may move.

## Testing

Prefer the in-memory store in unit tests to keep them deterministic and fast.

## Choosing a storage backend

In-memory is ideal for tests, JSON files suit small workloads, and SQLite scales to larger books while keeping a single-file deployment.

## Working with fiscal periods

A `Period` is half-open: the start is inclusive and the end exclusive, so adjacent periods never double-count a posting.

## Multicurrency

Amounts are always bound to a `Currency`; convert explicitly with `ExchangeRates` rather than mixing units implicitly.

## Error handling

Validation failures surface as `LedgerException` subclasses, so callers can catch `UnbalancedTransactionException` specifically.

## Thread safety

The core domain types are immutable; stores are not thread-safe by default and should be synchronized by the caller.

## Performance

Balance computation is O(entries); cache `Ledger.GetBalances()` when rendering several reports from the same journal.

## Precision

`Money` rounds to the currency's minor unit using banker-agnostic away-from-zero rounding.

## Report formats

Reports are returned as plain text; wrap them with your own HTML or PDF renderer.

## Migration

Between minor versions, stick to the public API in the root namespace; internals may move.

## Testing

Prefer the in-memory store in unit tests to keep them deterministic and fast.

## Choosing a storage backend

In-memory is ideal for tests, JSON files suit small workloads, and SQLite scales to larger books while keeping a single-file deployment.

## Working with fiscal periods

A `Period` is half-open: the start is inclusive and the end exclusive, so adjacent periods never double-count a posting.

## About this project

This repository is a **synthetic educational example**: the project and its
commit history are fictional, generated to illustrate realistic software
engineering workflows. See [HISTORY.md](HISTORY.md) for the simulated timeline.
