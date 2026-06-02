# Architecture Decision Records

This file tracks significant technical decisions made during the project. Each entry follows the ADR (Architecture Decision Record) format: **Context** describes the situation that forced a choice, **Decision** records what was chosen, and **Consequences** lists the trade-offs accepted.

Add a new entry whenever a meaningful architectural or technology choice is made. Number them sequentially.

---

## ADR-001 — Clean Architecture for the backend

**Date:** 2026-06-01  
**Status:** Accepted

### Context

The backend needed a structure that kept business logic independent of frameworks, databases, and HTTP concerns. The project also serves as a portfolio piece, so the architecture should demonstrate professional software design.

### Decision

Use Clean Architecture with four projects: Domain, Application, Infrastructure, and Api. Domain has zero dependencies. Application depends only on Domain and declares interfaces. Infrastructure implements those interfaces. Api wires everything together via dependency injection.

### Consequences

- **Positive:** Business logic (Application layer) is fully testable without a database or HTTP stack. Swapping PostgreSQL for another database would only affect Infrastructure. The project structure communicates intent clearly.
- **Negative:** More boilerplate than a simple layered or vertical-slice approach. For a small app like this, the overhead is noticeable — each feature needs a command/query, handler, validator, and DTO spread across files.
- **Accepted trade-off:** The boilerplate cost is worth it for the portfolio signal and the enforced separation of concerns.

---

## ADR-002 — Next.js 14 App Router for the frontend

**Date:** 2026-06-01  
**Status:** Accepted

### Context

The frontend needed to be a modern React application with TypeScript support, a good developer experience, and the ability to serve both auth pages and a rich dashboard. Two options were considered: Next.js 14 (App Router) and a plain Vite + React SPA.

### Decision

Use Next.js 14 with the App Router. All dashboard pages are Client Components because they require interactivity, React Query hooks, and real-time state. Auth pages are also Client Components for the same reason.

### Consequences

- **Positive:** File-based routing, built-in layout nesting, `next/navigation` for programmatic routing, and the standalone build output for Docker. The App Router's route groups (`(auth)`, `(dashboard)`) cleanly separate layout concerns.
- **Negative:** The App Router adds conceptual overhead (Server vs Client Components, `'use client'` directives). In this project nearly every page is a Client Component, so the Server Component benefits are not fully realised.
- **Accepted trade-off:** The App Router is the current standard for Next.js and is worth learning even if this project doesn't exploit all of its features.

---

<!-- Add new ADRs below this line -->
