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

## About this project

This repository is a **synthetic educational example**: the project and its
commit history are fictional, generated to illustrate realistic software
engineering workflows. See [HISTORY.md](HISTORY.md) for the simulated timeline.
