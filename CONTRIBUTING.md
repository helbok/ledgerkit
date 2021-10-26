# Contributing to LedgerKit

Thanks for your interest in contributing. LedgerKit is a small library and we
review contributions in a lightweight way.

## Getting started

1. Fork the repository and clone it locally.
2. Restore dependencies: `dotnet restore LedgerKit.sln`.
3. Build: `dotnet build LedgerKit.sln`.
4. Run tests: `dotnet test LedgerKit.sln`.

## Guidelines

- Follow the existing code style (4-space indent, `_`-free public API).
- Keep the public API small and typed.
- Add tests for new behaviour.
- Write conventional commits (`feat:`, `fix:`, `test:`, ...).

## Pull requests

Open a pull request against `main`. CI must pass before a merge.

## Questions

Open an issue with a clear reproduction where possible.
