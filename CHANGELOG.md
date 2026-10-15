# Changelog

All notable changes to this project are documented in this file. The format is
based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and this
project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.1.0] - 2019-06-27

### Added

- Core domain model (Account, Money, Entry, Transaction).
- Journal and Ledger balance computation.
- Initial test suite.

## [0.2.0] - 2019-10-22

### Added

- `ILedgerStore` contract with in-memory and JSON-file stores.
- Trial balance report generator.
- `Money.Abs` and typed balance lookup helpers.

## [0.3.0] - 2019-12-16

### Added

- `TrialBalance` snapshot type with debit/credit positions.
- Total row in trial balance report.

## [0.4.0] - 2020-04-13

### Added

- SQLite-backed store.
- `IncomeStatement` and `BalanceSheet` reports.
- `Period` type for fiscal periods.

## [0.5.0] - 2020-06-29

### Added

- Currency exchange rates with inverse lookup.
- Cash flow statement builder.
- Journal period range and per-account transaction queries.

## [0.6.0] - 2020-09-09

### Added

- Immutable `LedgerSnapshot` capture.
- Income statement and balance sheet reports via `ReportGenerator`.

### Fixed

- SQLite store now preserves account types on read.

## [0.7.0] - 2021-01-06

### Added

- Asynchronous store contract and in-memory implementation.

## [0.8.0] - 2021-06-10

### Changed

- Balance validation centralized in `Validation.RequireBalanced`.

## [1.0.0] - 2022-03-14

### Added

- Stable 1.0 public API.
- Async store support for in-memory and JSON stores.
- Contributor documentation and code of conduct.

## [1.1.0] - 2022-12-01

### Added

- Store/journal reconciliation helper.
- Async store support for SQLite.

### Fixed

- SQLite timestamps are now parsed as text rather than native dates.

## [1.2.0] - 2023-08-28

### Added

- Period-scoped balance lookups on `Ledger`.

### Changed

- SQLite entries are now indexed by transaction id.

## [2.0.0] - 2024-07-16

### Added

- Paginated SQLite transaction reads.

### Changed

- Now targets .NET 6 or later (breaking).
- SQLite amounts stored as text to preserve precision.

## [2.1.0] - 2025-05-08

### Added

- `Money.TryParse` for parsing amounts.

### Changed

- Now targets .NET 8 or later (breaking).

## [2.2.0] - 2026-10-07

### Changed

- Now targets .NET 9 or later (breaking).
- Expanded architecture, usage and installation documentation.

<!-- releases -->
