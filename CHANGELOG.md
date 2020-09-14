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

<!-- releases -->
