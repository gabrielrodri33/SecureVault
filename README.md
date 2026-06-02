# SecureVault

A full-stack password manager built with .NET 8 and Next.js 14. Store, organize, and share credentials securely with AES-256 encryption at rest.

## Stack

| Layer | Technology |
|-------|-----------|
| Backend | .NET 8, C#, Clean Architecture, MediatR (CQRS) |
| Frontend | Next.js 14 (App Router), TypeScript, TailwindCSS |
| Database | PostgreSQL 16 |
| Infrastructure | Docker, Docker Compose |

## Getting Started

```bash
# 1. Clone the repository
git clone <repo-url>
cd SecureVault

# 2. Copy environment variables
cp .env.example .env
# Edit .env and change JWT_SECRET and ENCRYPTION_KEY before running in production

# 3. Start everything
docker compose up --build
```

Services will be available at:
- **Frontend**: http://localhost:3000
- **API**: http://localhost:5000
- **Swagger UI**: http://localhost:5000/swagger

Default admin credentials (created on first run):
- Email: `admin@securevault.com`
- Password: `Admin@123`

## Project Structure

```
SecureVault/
├── src/
│   ├── SecureVault.Api/           # Controllers, middleware, DI, Program.cs
│   ├── SecureVault.Application/   # Use cases (CQRS), DTOs, validators, interfaces
│   ├── SecureVault.Domain/        # Entities, enums
│   └── SecureVault.Infrastructure/# EF Core, repositories, JWT, AES crypto
├── frontend/
│   ├── app/                       # Next.js App Router pages
│   ├── components/                # React components
│   ├── hooks/                     # React Query hooks
│   ├── lib/                       # Axios client, auth helpers
│   ├── store/                     # Zustand auth store
│   └── types/                     # TypeScript types
├── docker-compose.yml
├── .env.example
└── README.md
```

## API Routes

### Auth
| Method | Path | Description |
|--------|------|-------------|
| POST | `/api/auth/register` | Create a new account |
| POST | `/api/auth/login` | Sign in |
| POST | `/api/auth/refresh` | Refresh access token |
| POST | `/api/auth/logout` | Revoke refresh token |

### Vault Items
| Method | Path | Description |
|--------|------|-------------|
| GET | `/api/vault` | List items (search, filter, paginate) |
| GET | `/api/vault/{id}` | Get item with decrypted password |
| POST | `/api/vault` | Create item |
| PUT | `/api/vault/{id}` | Update item |
| DELETE | `/api/vault/{id}` | Delete item |
| GET | `/api/vault/favorites` | List favorites |
| PATCH | `/api/vault/{id}/favorite` | Toggle favorite |
| GET | `/api/vault/shared-with-me` | Items shared with you |
| POST | `/api/vault/{id}/share` | Share an item |
| DELETE | `/api/vault/{id}/share/{shareId}` | Remove a share |

### Collections
| Method | Path | Description |
|--------|------|-------------|
| GET | `/api/collections` | List collections |
| GET | `/api/collections/{id}/items` | Items in a collection |
| POST | `/api/collections` | Create collection |
| PUT | `/api/collections/{id}` | Update collection |
| DELETE | `/api/collections/{id}` | Delete collection |

### Admin (Admin role required)
| Method | Path | Description |
|--------|------|-------------|
| GET | `/api/admin/users` | List all users |
| GET | `/api/admin/users/{id}` | Get user details |
| PATCH | `/api/admin/users/{id}/status` | Toggle user active/inactive |
| GET | `/api/admin/stats` | Platform statistics |

## Development (without Docker)

**Backend:**
```bash
cd src/SecureVault.Api
# Update appsettings.Development.json with your local PostgreSQL connection
dotnet run
```

**Frontend:**
```bash
cd frontend
cp .env.local.example .env.local  # or create .env.local with NEXT_PUBLIC_API_URL=http://localhost:5000
npm install
npm run dev
```