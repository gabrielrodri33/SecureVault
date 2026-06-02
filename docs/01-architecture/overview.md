# Architecture Overview

## System Diagram

```
┌─────────────────────────────────────────────────────────────────┐
│                          Browser                                │
│                                                                 │
│   ┌─────────────────────────────────────────────────────────┐   │
│   │              Next.js 14 Frontend (port 3000)            │   │
│   │                                                         │   │
│   │   App Router pages  ·  React Query  ·  Zustand          │   │
│   │   React Hook Form   ·  Axios + interceptors             │   │
│   └──────────────────────────┬──────────────────────────────┘   │
└─────────────────────────────┼───────────────────────────────────┘
                              │  HTTP/REST  (JWT Bearer)
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│              .NET 8 Web API (port 5000)                         │
│                                                                 │
│   ┌───────────────┐   ┌───────────────┐   ┌─────────────────┐  │
│   │  SecureVault  │   │  SecureVault  │   │   SecureVault   │  │
│   │     .Api      │──▶│ .Application  │──▶│ .Infrastructure │  │
│   │               │   │  (MediatR)    │   │  (EF Core)      │  │
│   └───────────────┘   └──────┬────────┘   └────────┬────────┘  │
│                              │                     │            │
│                        ┌─────▼─────────────────────▼────────┐  │
│                        │       SecureVault.Domain            │  │
│                        │   Entities · Enums · (no deps)      │  │
│                        └─────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────────┘
                              │
                              │  TCP 5432
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│                    PostgreSQL 16 (port 5432)                     │
│            Users · VaultItems · Collections                      │
│            SharedItems · RefreshTokens                          │
└─────────────────────────────────────────────────────────────────┘
```

## Layers

| Layer | Responsibility |
|-------|---------------|
| **Frontend** | User interface, auth state, form validation, API communication |
| **Api** | HTTP routing, middleware, authentication enforcement, DI wiring |
| **Application** | Use cases (commands + queries), validation pipeline, business rules |
| **Domain** | Core entities and enums — no framework dependencies |
| **Infrastructure** | Database access, JWT generation, AES encryption, BCrypt hashing |
| **PostgreSQL** | Persistent storage for all application data |

## Detail Pages

- [Backend — Clean Architecture](backend.md)
- [Frontend — Next.js 14](frontend.md)
- [Database — Schema and migrations](database.md)
