# Changelog

All notable changes to SecureVault are documented here.

Format follows [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).  
Versioning follows [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [Unreleased]

### Added

### Changed

### Deprecated

### Removed

### Fixed

### Security

---

## [0.1.0] — 2026-06-01

Initial scaffolded application generated via Claude Code.

### Added

- **Backend:** .NET 8 Clean Architecture solution with four projects (Domain, Application, Infrastructure, Api)
- **Domain entities:** User, VaultItem, Collection, SharedItem, RefreshToken
- **Auth:** JWT Bearer access tokens (15 min) with refresh token rotation (7 days), BCrypt password hashing
- **Vault:** full CRUD for vault items, AES-256 encryption at rest, search and pagination, favorites toggle
- **Collections:** CRUD for grouping vault items
- **Sharing:** share vault items with other users (read-only or edit permissions)
- **Admin:** user list, user status toggle (activate/deactivate), platform statistics
- **Infrastructure:** EF Core with Npgsql PostgreSQL provider, Fluent API entity configuration, auto-migration on startup, admin seed data
- **Global error handling:** middleware returning RFC 7807 ProblemDetails
- **Swagger UI** with JWT Bearer authentication support
- **Frontend:** Next.js 14 App Router, TypeScript, TailwindCSS
- **Frontend pages:** Login, Register, Vault (all items), Favorites, Shared with me, Collections, Collection detail, Admin users, Admin stats
- **Auth flow:** silent token refresh via Axios interceptor, Zustand auth store with localStorage persistence
- **Forms:** React Hook Form + Zod validation for all forms
- **Docker Compose:** PostgreSQL 16, .NET API, Next.js frontend — single `docker compose up --build`
- **`.env.example`** with all required variables
- **README** with getting started and API route reference
