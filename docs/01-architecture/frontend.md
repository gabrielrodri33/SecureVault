# Frontend — Next.js 14

## App Router Structure

```
frontend/
├── app/
│   ├── layout.tsx                  # Root layout — wraps everything in <Providers>
│   ├── providers.tsx               # TanStack Query client provider
│   ├── globals.css
│   ├── (auth)/                     # Route group — no shared layout
│   │   ├── login/page.tsx
│   │   └── register/page.tsx
│   └── (dashboard)/                # Route group — shares Sidebar + auth guard
│       ├── layout.tsx              # Auth guard: redirects to /login if not authenticated
│       ├── page.tsx                # All vault items (search, filter, paginate)
│       ├── favorites/page.tsx
│       ├── shared/page.tsx
│       ├── collections/
│       │   ├── page.tsx
│       │   └── [id]/page.tsx
│       └── admin/
│           ├── page.tsx            # User management table
│           └── stats/page.tsx
├── components/
│   ├── ui/                         # Button, Input, Modal, Badge
│   ├── vault/                      # VaultItemCard, VaultItemDetail, VaultItemForm, PasswordReveal
│   ├── collections/                # CollectionCard, CollectionForm
│   └── layout/                     # Sidebar, Header
├── hooks/
│   ├── useAuth.ts                  # useLogin, useRegister, useLogout
│   ├── useVault.ts                 # useVaultItems, useVaultItem, useCreateVaultItem, ...
│   └── useCollections.ts
├── lib/
│   ├── api.ts                      # Axios instance with request/response interceptors
│   └── auth.ts                     # localStorage token read/write helpers
├── store/
│   └── authStore.ts                # Zustand store (user, isAuthenticated, login, logout)
├── types/
│   └── index.ts                    # All TypeScript interfaces
├── middleware.ts
└── next.config.js
```

## State Management

Two layers handle state:

| Concern | Library | Where |
|---------|---------|-------|
| Auth session (user, tokens) | Zustand + `persist` middleware | `store/authStore.ts` — survives page reloads via localStorage |
| Server data (vault items, collections, etc.) | TanStack Query (React Query) | `hooks/` — cached, auto-invalidated on mutations |

This separation means auth state is synchronous and always available without a network round-trip, while server state is always fresh without manual cache management.

## Auth Flow

1. Login/Register → API returns `accessToken` + `refreshToken` + `user`
2. Zustand stores `user` and sets `isAuthenticated = true`; tokens go to localStorage
3. Axios request interceptor attaches `Authorization: Bearer <accessToken>` to every request
4. On 401, the response interceptor calls `/api/auth/refresh`, updates tokens, and retries the original request transparently
5. Dashboard layout checks `isAuthenticated` — if false, redirects to `/login`

## Key Libraries

| Library | Purpose |
|---------|---------|
| Next.js 14 | App Router, server/client components, file-based routing |
| TypeScript | Static typing across all files |
| TailwindCSS | Utility-first styling |
| TanStack Query v5 | Server state — fetching, caching, invalidation |
| Zustand | Client state — auth session |
| Axios | HTTP client with interceptors for auth |
| React Hook Form | Form state and submission |
| Zod | Schema validation for forms |
| @hookform/resolvers | Connects Zod schemas to React Hook Form |
| lucide-react | Icon set |
| clsx | Conditional class name utility |
