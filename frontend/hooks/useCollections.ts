import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { api } from '@/lib/api';
import { Collection, VaultItem } from '@/types';

export function useCollections() {
  return useQuery({ queryKey: ['collections'], queryFn: () => api.get<Collection[]>('/api/collections').then(r => r.data) });
}

export function useCollectionItems(id: string) {
  return useQuery({ queryKey: ['collections', id, 'items'], queryFn: () => api.get<VaultItem[]>(`/api/collections/${id}/items`).then(r => r.data), enabled: !!id });
}

export function useCreateCollection() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (data: { name: string; description?: string }) => api.post<Collection>('/api/collections', data).then(r => r.data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['collections'] }),
  });
}

export function useUpdateCollection() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ id, ...data }: { id: string; name: string; description?: string }) => api.put<Collection>(`/api/collections/${id}`, data).then(r => r.data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['collections'] }),
  });
}

export function useDeleteCollection() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => api.delete(`/api/collections/${id}`),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['collections'] }),
  });
}
