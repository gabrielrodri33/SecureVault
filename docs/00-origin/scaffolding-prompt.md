# Scaffolding Prompts

This file captures the two prompts used to bootstrap the SecureVault project via Claude Code — the application scaffolding prompt and the documentation scaffolding prompt.

---

## Prompt 1 — Application Scaffold

| Field | Value |
|-------|-------|
| Date | 2026-06-01 |
| Model | Claude Sonnet 4.6 (claude-sonnet-4-6) |
| Tool | Claude Code (VSCode extension) |
| Working directory | `d:\SecureVault` |

### Execution context

Submitted in a single message and executed using three parallel specialised agents:

- **backend-dotnet** — .NET 8 solution, Clean Architecture, all CQRS handlers, EF Core, JWT, AES-256
- **frontend-nextjs** — Next.js 14 App Router, TanStack Query, Zustand, React Hook Form
- **devops** — Docker Compose, Dockerfiles, `.env.example`, README

Build completed without errors. EF Core initial migration (`InitialCreate`) was generated as part of the backend agent run.

### Prompt text

```
Build a full-stack password manager application called **SecureVault**.

## Stack

- **Backend**: .NET 8, C#, Clean Architecture
- **Frontend**: Next.js 14 (App Router), TypeScript, TailwindCSS
- **Database**: PostgreSQL
- **Infrastructure**: Docker + Docker Compose

---

## Backend — Clean Architecture

Create a solution with four projects:

```
SecureVault.sln
├── src/
│   ├── SecureVault.Api/           # Controllers, middleware, DI setup
│   ├── SecureVault.Application/   # Use cases, DTOs, interfaces, validators
│   ├── SecureVault.Domain/        # Entities, enums, domain events
│   └── SecureVault.Infrastructure/# EF Core, repositories, JWT, crypto helpers
```

### Domain entities

- **User** — Id, Email, PasswordHash, Name, CreatedAt, UpdatedAt
- **Collection** — Id, UserId, Name, Description, CreatedAt
- **VaultItem** — Id, UserId, CollectionId (nullable), Title, Username, EncryptedPassword, Url, Notes, IsFavorite, CreatedAt, UpdatedAt
- **SharedItem** — Id, VaultItemId, SharedWithUserId, CanEdit, CreatedAt

### API endpoints

Auth:
- POST /api/auth/register
- POST /api/auth/login
- POST /api/auth/refresh
- POST /api/auth/logout

Vault items:
- GET    /api/vault                      (list all, with filtering + pagination)
- GET    /api/vault/{id}
- POST   /api/vault
- PUT    /api/vault/{id}
- DELETE /api/vault/{id}
- GET    /api/vault/favorites
- PATCH  /api/vault/{id}/favorite

Collections:
- GET    /api/collections
- GET    /api/collections/{id}/items
- POST   /api/collections
- PUT    /api/collections/{id}
- DELETE /api/collections/{id}

Sharing:
- POST   /api/vault/{id}/share
- GET    /api/vault/shared-with-me
- DELETE /api/vault/{id}/share/{shareId}

Admin (admin role only):
- GET    /api/admin/users
- GET    /api/admin/users/{id}
- PATCH  /api/admin/users/{id}/status    (activate / deactivate)
- GET    /api/admin/stats

### Technical requirements

- Use Entity Framework Core with PostgreSQL (Npgsql provider)
- Fluent API for entity configuration
- Repository pattern + Unit of Work
- MediatR for CQRS (commands and queries in Application layer)
- FluentValidation for request validation
- JWT Bearer authentication with refresh token support (store refresh tokens in DB)
- Global exception handler returning RFC 7807 ProblemDetails
- Auto-apply migrations on startup
- Seed a default admin user on first run

---

## Frontend — Next.js 14

```
frontend/
├── app/
│   ├── (auth)/
│   │   ├── login/page.tsx
│   │   └── register/page.tsx
│   ├── (dashboard)/
│   │   ├── layout.tsx             # sidebar + header
│   │   ├── page.tsx               # vault home — all items
│   │   ├── favorites/page.tsx
│   │   ├── shared/page.tsx
│   │   ├── collections/
│   │   │   ├── page.tsx
│   │   │   └── [id]/page.tsx
│   │   └── admin/
│   │       ├── page.tsx           # user management
│   │       └── stats/page.tsx
├── components/
│   ├── vault/
│   │   ├── VaultItemCard.tsx
│   │   ├── VaultItemForm.tsx
│   │   ├── VaultItemDetail.tsx
│   │   └── PasswordReveal.tsx     # toggle show/hide password
│   ├── collections/
│   │   ├── CollectionCard.tsx
│   │   └── CollectionForm.tsx
│   ├── layout/
│   │   ├── Sidebar.tsx
│   │   ├── Header.tsx
│   │   └── PageHeader.tsx
│   └── ui/                        # Button, Input, Modal, Badge, etc.
├── lib/
│   ├── api.ts                     # Axios instance with interceptors + token refresh
│   └── auth.ts                    # Token helpers
├── hooks/
│   ├── useVault.ts
│   ├── useCollections.ts
│   └── useAuth.ts
└── types/
    └── index.ts
```

### Pages behavior

- **Login / Register**: clean centered form, redirect to dashboard on success
- **Dashboard (/)**: search bar, filter by collection, grid/list toggle, cards showing title + username + url (password hidden), copy-to-clipboard button on each card
- **Vault item detail**: slide-over panel or modal with full details, password reveal toggle, edit and delete actions
- **Collections**: collection list, click to open and view items inside
- **Shared with me**: items other users shared, read-only if canEdit is false
- **Admin — Users**: table with all users, status badge (active/inactive), toggle status action
- **Admin — Stats**: cards showing total users, total items, total collections

### Technical requirements

- Use React Query (TanStack Query) for all data fetching
- Axios with request/response interceptors for JWT attach and silent refresh
- Zustand for auth state (user, tokens)
- React Hook Form + Zod for all forms
- Protect routes with a middleware that checks auth state; redirect to login if unauthenticated
- Admin routes additionally check user role; redirect to dashboard if not admin
- Use next/navigation for routing

---

## Docker Compose

Provide a `docker-compose.yml` at the root with three services:

- **postgres**: postgres:16-alpine, volume for data persistence, env vars for DB name/user/password
- **api**: builds from `./src/SecureVault.Api`, depends on postgres, exposes port 5000
- **frontend**: builds from `./frontend`, depends on api, exposes port 3000

Include a `.env.example` with all required variables:
- `POSTGRES_DB`, `POSTGRES_USER`, `POSTGRES_PASSWORD`
- `JWT_SECRET`, `JWT_EXPIRY_MINUTES`, `JWT_REFRESH_EXPIRY_DAYS`
- `NEXT_PUBLIC_API_URL`

---

## README

Create a `README.md` covering:
- What SecureVault is
- Tech stack overview
- Getting started (clone → copy `.env.example` → `docker compose up`)
- Project structure summary
- Available API routes

---

## What NOT to include

- No security hardening or security headers configuration
- No rate limiting, no IP blocking
- No CI/CD workflows, no GitHub Actions
- No Dockerfile security best practices (non-root users, etc.)
- No secrets scanning, SAST, DAST tools
- No Terraform or infrastructure-as-code
- No penetration testing documentation
- No OWASP references

The goal is a clean, well-structured, fully functional application that runs with a single `docker compose up --build`.
```

---

## Prompt 2 — Documentation Scaffold

| Field | Value |
|-------|-------|
| Date | 2026-06-01 |
| Model | Claude Sonnet 4.6 (claude-sonnet-4-6) |
| Tool | Claude Code (VSCode extension) |
| Working directory | `d:\SecureVault` |

### Execution context

Submitted after the application scaffold was complete. Executed inline (no subagents) — all 15 files written directly by the main Claude Code session.

### Prompt text

```
Create a `/docs` folder in the root of the project with a structured documentation setup for the SecureVault project lifecycle — from the original scaffolding prompt to the end of the project.

## Folder structure

docs/
├── index.md
├── 00-origin/
│   ├── project-concept.md
│   └── scaffolding-prompt.md
├── 01-architecture/
│   ├── overview.md
│   ├── backend.md
│   ├── frontend.md
│   └── database.md
├── 02-development/
│   ├── decisions.md
│   └── changelog.md
├── 03-api/
│   └── endpoints.md
├── 04-security/
│   ├── threat-model.md
│   ├── vulnerabilities/
│   │   └── VULN-TEMPLATE.md
│   └── fixes/
│       └── FIX-TEMPLATE.md
└── 05-pentest/
    ├── methodology.md
    └── findings/
        └── FINDING-TEMPLATE.md

## File contents

### `docs/index.md`
Project documentation index. Brief description of SecureVault, link to each section, and a project status table:

| Phase | Status | Description |
|---|---|---|
| Scaffolding | Done | Initial app generated via Claude Code |
| Architecture | In progress | — |
| Development | In progress | — |
| API Documentation | In progress | — |
| Security Analysis | Pending | — |
| Pentest | Pending | — |
| Fixes | Pending | — |

### `docs/00-origin/project-concept.md`
Describe the project concept:
- What SecureVault is
- Why it was built (portfolio + DevSecOps learning path)
- Goals: build → pentest → document → fix → document again
- Stack chosen and rationale

### `docs/00-origin/scaffolding-prompt.md`
A placeholder file with a clear heading and a note that the original Claude Code prompt used to scaffold the project should be pasted here. Include a section for date, model used, and any relevant context about the generation.

### `docs/01-architecture/overview.md`
High-level architecture overview:
- System diagram in ASCII showing Frontend → API → Database
- Brief description of each layer
- Link to backend.md, frontend.md, database.md

### `docs/01-architecture/backend.md`
Document the .NET 8 Clean Architecture:
- The four projects and their responsibilities
- Folder structure of each project
- Dependency direction rules (Domain ← Application ← Infrastructure → Api)
- Key libraries used: MediatR, EF Core, FluentValidation, JWT

### `docs/01-architecture/frontend.md`
Document the Next.js 14 frontend:
- App Router structure
- State management approach (Zustand + React Query)
- Folder structure explanation
- Key libraries used

### `docs/01-architecture/database.md`
Document the database:
- Entity relationship diagram in ASCII
- Table descriptions
- Migration strategy

### `docs/02-development/decisions.md`
Architecture Decision Records (ADR) format. Pre-populate with two example entries:

**ADR-001 — Clean Architecture for backend**
- Context, Decision, Consequences

**ADR-002 — Next.js App Router**
- Context, Decision, Consequences

Add a note at the top explaining the ADR format and that new decisions should be added here as the project evolves.

### `docs/02-development/changelog.md`
Changelog following Keep a Changelog format (keepachangelog.com). Start with a `[Unreleased]` section and an initial `[0.1.0]` entry for the scaffolded application.

### `docs/03-api/endpoints.md`
Complete API reference table:
- All endpoints organized by group (Auth, Vault, Collections, Sharing, Admin)
- Columns: Method, Route, Auth required, Description
- Request/response body examples in JSON for the main endpoints (register, login, create vault item, create collection)

### `docs/04-security/threat-model.md`
Placeholder file for the threat model. Include headings for:
- Assets
- Threat actors
- STRIDE analysis table (empty, to be filled)
- Mitigations (empty, to be filled)

Add a note that this section will be filled after the pentest phase.

### `docs/04-security/vulnerabilities/VULN-TEMPLATE.md`
Template for documenting a found vulnerability.

### `docs/04-security/fixes/FIX-TEMPLATE.md`
Template for documenting a security fix.

### `docs/05-pentest/methodology.md`
Placeholder file for the pentest methodology. Include headings for:
- Scope
- Tools used
- Testing approach
- Out of scope

### `docs/05-pentest/findings/FINDING-TEMPLATE.md`
Template for a pentest finding.

---

## Requirements

- All files must use standard GitHub-flavored markdown
- Use relative links between files where relevant
- Do not add any content about security tooling, CI/CD, or DevSecOps pipelines
- The docs folder should feel like a living document that grows alongside the project
```
