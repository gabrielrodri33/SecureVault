export interface User {
  id: string;
  email: string;
  name: string;
  role: 'User' | 'Admin';
}

export interface AuthResponse {
  accessToken: string;
  refreshToken: string;
  user: User;
}

export interface VaultItem {
  id: string;
  title: string;
  username: string | null;
  url: string | null;
  isFavorite: boolean;
  collectionId: string | null;
  createdAt: string;
  updatedAt: string;
}

export interface VaultItemDetail extends VaultItem {
  password: string;
  notes: string | null;
}

export interface PaginatedResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
}

export interface Collection {
  id: string;
  name: string;
  description: string | null;
  itemCount: number;
  createdAt: string;
}

export interface SharedVaultItem {
  shareId: string;
  itemId: string;
  title: string;
  username: string | null;
  url: string | null;
  isFavorite: boolean;
  sharedBy: User;
  canEdit: boolean;
  sharedAt: string;
}

export interface AdminUser {
  id: string;
  email: string;
  name: string;
  role: string;
  isActive: boolean;
  createdAt: string;
}

export interface Stats {
  totalUsers: number;
  totalItems: number;
  totalCollections: number;
}
