import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { api } from '@/lib/api';
import { PaginatedResult, VaultItem, VaultItemDetail } from '@/types';

export function useVaultItems(search?: string, collectionId?: string, page = 1, pageSize = 20) {
  return useQuery({
    queryKey: ['vault', search, collectionId, page, pageSize],
    queryFn: () => api.get<PaginatedResult<VaultItem>>('/api/vault', { params: { search, collectionId, page, pageSize } }).then(r => r.data),
  });
}

export function useVaultItem(id: string) {
  return useQuery({
    queryKey: ['vault', id],
    queryFn: () => api.get<VaultItemDetail>(`/api/vault/${id}`).then(r => r.data),
    enabled: !!id,
  });
}

export function useFavorites() {
  return useQuery({ queryKey: ['vault', 'favorites'], queryFn: () => api.get<VaultItem[]>('/api/vault/favorites').then(r => r.data) });
}

export function useSharedWithMe() {
  return useQuery({ queryKey: ['vault', 'shared'], queryFn: () => api.get('/api/vault/shared-with-me').then(r => r.data) });
}

export function useCreateVaultItem() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (data: { title: string; username?: string; password: string; url?: string; notes?: string; collectionId?: string; isFavorite?: boolean }) =>
      api.post<VaultItem>('/api/vault', data).then(r => r.data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['vault'] }),
  });
}

export function useUpdateVaultItem() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ id, ...data }: { id: string; title: string; username?: string; password: string; url?: string; notes?: string; collectionId?: string; isFavorite: boolean }) =>
      api.put<VaultItem>(`/api/vault/${id}`, data).then(r => r.data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['vault'] }),
  });
}

export function useDeleteVaultItem() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => api.delete(`/api/vault/${id}`),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['vault'] }),
  });
}

export function useToggleFavorite() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => api.patch(`/api/vault/${id}/favorite`),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['vault'] }),
  });
}

export function useShareVaultItem() {
  return useMutation({
    mutationFn: ({ id, ...data }: { id: string; sharedWithUserEmail: string; canEdit: boolean }) =>
      api.post(`/api/vault/${id}/share`, data).then(r => r.data),
  });
}
