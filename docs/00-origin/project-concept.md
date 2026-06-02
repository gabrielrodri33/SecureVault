# Project Concept

## What is SecureVault?

SecureVault is a web-based password manager. Users can store, organise, and share credentials securely. Passwords are encrypted at rest using AES-256; user account passwords are hashed with BCrypt. Authentication is JWT-based with refresh token rotation.

The application is intentionally feature-complete enough to be realistic — it has user accounts, sharing, collections, admin controls, and a proper frontend — which makes it a meaningful target for security analysis.

## Why It Was Built

SecureVault was built as a portfolio project with a deliberate DevSecOps learning path in mind:

1. **Build** — scaffold a realistic, well-structured application
2. **Document** — capture architecture, decisions, and API contracts
3. **Pentest** — treat the app as a black/grey-box target and find real vulnerabilities
4. **Fix** — apply and document fixes
5. **Document again** — record what changed and why

A password manager is a good candidate for this exercise because it handles sensitive data, involves authentication and authorisation, exposes a REST API, and has enough surface area (CRUD, sharing, admin roles) to surface a variety of vulnerability classes.

## Goals

- Demonstrate full-stack engineering across a realistic technology stack
- Practice Clean Architecture and CQRS patterns in a real project context
- Build a documented security analysis lifecycle from scratch
- Produce reusable templates for vulnerability and fix documentation

## Stack and Rationale

| Layer | Technology | Rationale |
|-------|-----------|-----------|
| Backend | .NET 8, C# | Strongly typed, production-grade, Clean Architecture fits well |
| API pattern | CQRS with MediatR | Separates read/write concerns, scales well, testable |
| ORM | Entity Framework Core + Npgsql | First-class PostgreSQL support, code-first migrations |
| Validation | FluentValidation | Expressive rules, integrates cleanly with MediatR pipeline |
| Frontend | Next.js 14 (App Router) | Modern React, file-based routing, TypeScript first-class |
| UI | TailwindCSS | Utility-first, fast to iterate |
| State | Zustand + TanStack Query | Zustand for auth/session, React Query for server state |
| Forms | React Hook Form + Zod | Type-safe, minimal re-renders |
| Database | PostgreSQL 16 | Reliable, well-supported, good EF Core provider |
| Infra | Docker Compose | Single command to run the full stack locally |
