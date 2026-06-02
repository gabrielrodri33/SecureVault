# Database

## Entity Relationship Diagram

```
┌──────────────────┐        ┌──────────────────────┐
│      Users       │        │     RefreshTokens     │
│──────────────────│        │──────────────────────│
│ Id (PK)          │◄───────│ UserId (FK)           │
│ Email (unique)   │        │ Id (PK)               │
│ PasswordHash     │        │ Token (unique)        │
│ Name             │        │ ExpiresAt             │
│ Role             │        │ IsRevoked             │
│ IsActive         │        │ CreatedAt             │
│ CreatedAt        │        └──────────────────────┘
│ UpdatedAt        │
└────────┬─────────┘
         │ 1
         │
    ┌────┴─────────────────────────────────────────┐
    │                                              │
    │ n                                            │ n
┌───▼──────────────┐        ┌─────────────────────▼──┐
│   Collections    │        │       VaultItems        │
│──────────────────│        │────────────────────────│
│ Id (PK)          │        │ Id (PK)                 │
│ UserId (FK)      │        │ UserId (FK)             │
│ Name             │◄───────│ CollectionId (FK, null) │
│ Description      │        │ Title                   │
│ CreatedAt        │        │ Username                │
└──────────────────┘        │ EncryptedPassword       │
                            │ Url                     │
                            │ Notes                   │
                            │ IsFavorite              │
                            │ CreatedAt               │
                            │ UpdatedAt               │
                            └────────────┬────────────┘
                                         │ 1
                                         │
                                         │ n
                            ┌────────────▼────────────┐
                            │       SharedItems        │
                            │────────────────────────│
                            │ Id (PK)                 │
                            │ VaultItemId (FK)        │
                            │ SharedWithUserId (FK)   │
                            │ CanEdit                 │
                            │ CreatedAt               │
                            └─────────────────────────┘
                              unique (VaultItemId, SharedWithUserId)
```

## Tables

### Users
Stores accounts. `Role` is stored as a string (`User` or `Admin`). `IsActive` can be toggled by admins to disable accounts without deleting them.

### VaultItems
Core entity. `EncryptedPassword` is the AES-256 CBC ciphertext (base64 encoded, IV prepended). `CollectionId` is nullable — an item may or may not belong to a collection. Indexed on `UserId` and `(UserId, IsFavorite)`.

### Collections
Named groups of vault items belonging to a user. Deleting a collection cascades to `VaultItems` (sets `CollectionId = NULL` via EF's cascade configuration).

### SharedItems
Join table for the sharing feature. The unique index on `(VaultItemId, SharedWithUserId)` prevents duplicate shares. `CanEdit` controls whether the recipient can modify the item.

### RefreshTokens
Stores refresh token strings for the JWT refresh flow. `IsRevoked` is set to `true` on logout or on token rotation (after a successful refresh the old token is revoked and a new one is issued). Indexed on `(UserId, IsRevoked)` for efficient lookup.

## Migration Strategy

Migrations are managed by EF Core and applied automatically at API startup via `context.Database.MigrateAsync()` in `Program.cs`.

To create a new migration after a schema change:

```bash
dotnet ef migrations add <MigrationName> \
  --project src/SecureVault.Infrastructure \
  --startup-project src/SecureVault.Api \
  --output-dir Data/Migrations
```

The initial migration is `20260602010141_InitialCreate` and covers all five tables above.
