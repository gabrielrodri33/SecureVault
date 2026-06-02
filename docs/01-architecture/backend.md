# Backend — Clean Architecture

## Projects and Responsibilities

```
SecureVault.sln
└── src/
    ├── SecureVault.Domain/          # Core — no external dependencies
    ├── SecureVault.Application/     # Use cases — depends on Domain only
    ├── SecureVault.Infrastructure/  # Data access + services — depends on Application
    └── SecureVault.Api/             # HTTP layer — depends on Application + Infrastructure
```

### SecureVault.Domain

The innermost layer. Contains entities and enums only. Has no NuGet dependencies and no references to other projects.

```
Domain/
├── Entities/
│   ├── User.cs
│   ├── VaultItem.cs
│   ├── Collection.cs
│   ├── SharedItem.cs
│   └── RefreshToken.cs
└── Enums/
    └── UserRole.cs
```

### SecureVault.Application

Orchestrates use cases via CQRS. Each feature is a self-contained folder with a command/query, validator, and handler.

```
Application/
├── Common/
│   ├── Behaviors/         # ValidationBehavior (MediatR pipeline)
│   ├── Exceptions/        # NotFoundException, ForbiddenException, UnauthorizedException
│   └── Interfaces/        # IApplicationDbContext, IJwtService, ICryptoService, ICurrentUserService
├── Features/
│   ├── Auth/              # Register, Login, Refresh, Logout
│   ├── Vault/             # CRUD, ToggleFavorite, GetFavorites, GetVaultItems (paginated)
│   ├── Collections/       # CRUD, GetCollectionItems
│   ├── Sharing/           # ShareVaultItem, DeleteShare, GetSharedWithMe
│   └── Admin/             # GetUsers, GetUser, ToggleUserStatus, GetStats
└── DependencyInjection.cs
```

### SecureVault.Infrastructure

Implements the interfaces declared in Application. Owns the database context, EF configurations, and all external service wrappers.

```
Infrastructure/
├── Data/
│   ├── ApplicationDbContext.cs
│   ├── Configurations/    # Fluent API entity configs (one per entity)
│   ├── Migrations/        # EF Core auto-generated migrations
│   └── SeedData.cs        # Creates default admin on first run
├── Services/
│   ├── JwtService.cs      # Token generation and validation
│   ├── CryptoService.cs   # AES-256 encrypt/decrypt + BCrypt
│   └── CurrentUserService.cs
└── DependencyInjection.cs
```

### SecureVault.Api

The HTTP entry point. Wires up DI, middleware, Swagger, and maps routes to controllers.

```
Api/
├── Controllers/
│   ├── AuthController.cs
│   ├── VaultController.cs
│   ├── CollectionsController.cs
│   └── AdminController.cs
├── Middleware/
│   └── ExceptionHandlingMiddleware.cs   # Returns RFC 7807 ProblemDetails
├── appsettings.json
├── appsettings.Development.json
└── Program.cs
```

## Dependency Direction

```
SecureVault.Api
    ├── depends on → SecureVault.Application
    └── depends on → SecureVault.Infrastructure

SecureVault.Infrastructure
    └── depends on → SecureVault.Application

SecureVault.Application
    └── depends on → SecureVault.Domain

SecureVault.Domain
    └── (no dependencies)
```

Interfaces live in Application; implementations live in Infrastructure. The Api project wires them together via DI — Application never sees Infrastructure directly.

## Key Libraries

| Library | Version | Purpose |
|---------|---------|---------|
| MediatR | 12.2.0 | CQRS dispatch — controllers send commands/queries, handlers process them |
| FluentValidation | 11.9.0 | Request validation — runs as a MediatR pipeline behavior before every handler |
| Npgsql.EntityFrameworkCore.PostgreSQL | 8.0.4 | EF Core provider for PostgreSQL |
| Microsoft.EntityFrameworkCore | 8.0.4 | ORM — code-first migrations, Fluent API configuration |
| BCrypt.Net-Next | 4.0.3 | User password hashing |
| Microsoft.AspNetCore.Authentication.JwtBearer | 8.0.4 | JWT Bearer middleware |
| System.IdentityModel.Tokens.Jwt | 8.0.1 | JWT creation and validation |
| Swashbuckle.AspNetCore | 6.5.0 | Swagger / OpenAPI docs |
