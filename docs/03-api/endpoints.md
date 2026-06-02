# API Reference

Base URL: `http://localhost:5000`  
All authenticated endpoints require `Authorization: Bearer <accessToken>`.

---

## Auth

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| POST | `/api/auth/register` | No | Create a new user account |
| POST | `/api/auth/login` | No | Sign in, receive tokens |
| POST | `/api/auth/refresh` | No | Exchange a refresh token for new tokens |
| POST | `/api/auth/logout` | Yes | Revoke the current refresh token |

### POST `/api/auth/register`

**Request**
```json
{
  "name": "Alice",
  "email": "alice@example.com",
  "password": "MyPassword1!"
}
```

**Response 200**
```json
{
  "accessToken": "eyJhbGci...",
  "refreshToken": "base64string...",
  "user": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "email": "alice@example.com",
    "name": "Alice",
    "role": "User"
  }
}
```

### POST `/api/auth/login`

**Request**
```json
{
  "email": "alice@example.com",
  "password": "MyPassword1!"
}
```

**Response 200** — same shape as `/register`

---

## Vault Items

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| GET | `/api/vault` | Yes | List items (search, collectionId, page, pageSize) |
| GET | `/api/vault/{id}` | Yes | Get item with decrypted password |
| POST | `/api/vault` | Yes | Create item |
| PUT | `/api/vault/{id}` | Yes | Update item |
| DELETE | `/api/vault/{id}` | Yes | Delete item |
| GET | `/api/vault/favorites` | Yes | List favourited items |
| PATCH | `/api/vault/{id}/favorite` | Yes | Toggle favourite status |
| GET | `/api/vault/shared-with-me` | Yes | Items other users shared with you |
| POST | `/api/vault/{id}/share` | Yes | Share an item with another user |
| DELETE | `/api/vault/{id}/share/{shareId}` | Yes | Remove a share |

### GET `/api/vault`

Query parameters: `search` (string), `collectionId` (UUID), `page` (int, default 1), `pageSize` (int, default 20).

**Response 200**
```json
{
  "items": [
    {
      "id": "...",
      "title": "GitHub",
      "username": "alice",
      "url": "https://github.com",
      "isFavorite": false,
      "collectionId": null,
      "createdAt": "2026-06-01T00:00:00Z",
      "updatedAt": "2026-06-01T00:00:00Z"
    }
  ],
  "totalCount": 42,
  "page": 1,
  "pageSize": 20
}
```

### POST `/api/vault`

**Request**
```json
{
  "title": "GitHub",
  "username": "alice",
  "password": "s3cret!",
  "url": "https://github.com",
  "notes": "Work account",
  "collectionId": null,
  "isFavorite": false
}
```

**Response 201** — returns the created `VaultItemDto` (password not included).

---

## Collections

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| GET | `/api/collections` | Yes | List all collections for the current user |
| GET | `/api/collections/{id}/items` | Yes | List vault items inside a collection |
| POST | `/api/collections` | Yes | Create a collection |
| PUT | `/api/collections/{id}` | Yes | Update a collection |
| DELETE | `/api/collections/{id}` | Yes | Delete a collection |

### POST `/api/collections`

**Request**
```json
{
  "name": "Work",
  "description": "Work-related credentials"
}
```

**Response 201**
```json
{
  "id": "...",
  "name": "Work",
  "description": "Work-related credentials",
  "itemCount": 0,
  "createdAt": "2026-06-01T00:00:00Z"
}
```

---

## Sharing

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| POST | `/api/vault/{id}/share` | Yes | Share an item with another user by email |
| GET | `/api/vault/shared-with-me` | Yes | Items shared with the current user |
| DELETE | `/api/vault/{id}/share/{shareId}` | Yes | Remove a specific share |

### POST `/api/vault/{id}/share`

**Request**
```json
{
  "sharedWithUserEmail": "bob@example.com",
  "canEdit": false
}
```

**Response 201**
```json
{
  "shareId": "..."
}
```

---

## Admin

Requires `Admin` role. Returns 403 for non-admin users.

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| GET | `/api/admin/users` | Admin | List all users |
| GET | `/api/admin/users/{id}` | Admin | Get a single user |
| PATCH | `/api/admin/users/{id}/status` | Admin | Toggle user active/inactive |
| GET | `/api/admin/stats` | Admin | Platform-wide statistics |

### GET `/api/admin/stats`

**Response 200**
```json
{
  "totalUsers": 12,
  "totalItems": 304,
  "totalCollections": 28
}
```

---

## Error Responses

All errors follow RFC 7807 ProblemDetails:

```json
{
  "type": "https://httpstatuses.com/400",
  "title": "Validation failed",
  "status": 400,
  "errors": {
    "Password": ["Minimum length is 8 characters"]
  }
}
```

| Status | Trigger |
|--------|---------|
| 400 | Validation failure or invalid operation |
| 401 | Missing or invalid JWT / invalid credentials |
| 403 | Authenticated but insufficient permissions |
| 404 | Resource not found |
| 500 | Unhandled server error |
