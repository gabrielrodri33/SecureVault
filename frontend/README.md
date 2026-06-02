# SecureVault Frontend

Next.js 14 password manager frontend with TypeScript, Tailwind CSS, and TanStack Query.

## Tech Stack

- **Framework**: Next.js 14 (App Router)
- **Language**: TypeScript
- **Styling**: Tailwind CSS
- **State Management**: Zustand (auth), TanStack Query (server state)
- **Forms**: React Hook Form + Zod validation
- **HTTP Client**: Axios with automatic token refresh
- **Icons**: Lucide React

## Installation

```bash
npm install
```

## Environment Variables

Create `.env.local`:

```env
NEXT_PUBLIC_API_URL=http://localhost:5000
```

## Development

```bash
npm run dev
```

Open http://localhost:3000

## Build

```bash
npm run build
npm start
```

## Features

- JWT authentication with automatic token refresh
- Password vault (CRUD operations)
- Favorites and Collections
- Item sharing
- Search and pagination
- Admin dashboard
- Responsive design
