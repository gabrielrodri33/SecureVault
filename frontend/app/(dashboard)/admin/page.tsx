'use client';
import { useEffect } from 'react';
import { useRouter } from 'next/navigation';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { api } from '@/lib/api';
import { useAuthStore } from '@/store/authStore';
import { AdminUser } from '@/types';
import { Badge } from '@/components/ui/Badge';
import { Button } from '@/components/ui/Button';
import { Header } from '@/components/layout/Header';

export default function AdminUsersPage() {
  const { user } = useAuthStore();
  const router = useRouter();
  const qc = useQueryClient();

  useEffect(() => { if (user && user.role !== 'Admin') router.push('/'); }, [user, router]);

  const { data, isLoading } = useQuery({ queryKey: ['admin', 'users'], queryFn: () => api.get<AdminUser[]>('/api/admin/users').then(r => r.data) });
  const toggle = useMutation({ mutationFn: (id: string) => api.patch(`/api/admin/users/${id}/status`).then(r => r.data), onSuccess: () => qc.invalidateQueries({ queryKey: ['admin'] }) });

  return (
    <div>
      <Header title="User Management" />
      {isLoading ? <div className="flex justify-center py-12"><div className="animate-spin h-8 w-8 border-4 border-blue-600 border-t-transparent rounded-full" /></div> : (
        <div className="bg-white rounded-xl border border-gray-200 overflow-hidden">
          <table className="w-full">
            <thead className="bg-gray-50 border-b border-gray-200">
              <tr>
                {['Name', 'Email', 'Role', 'Status', 'Joined', 'Actions'].map(h => <th key={h} className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">{h}</th>)}
              </tr>
            </thead>
            <tbody className="divide-y divide-gray-100">
              {data?.map(u => (
                <tr key={u.id} className="hover:bg-gray-50">
                  <td className="px-4 py-3 text-sm font-medium text-gray-900">{u.name}</td>
                  <td className="px-4 py-3 text-sm text-gray-500">{u.email}</td>
                  <td className="px-4 py-3"><Badge label={u.role} variant={u.role === 'Admin' ? 'blue' : 'gray'} /></td>
                  <td className="px-4 py-3"><Badge label={u.isActive ? 'Active' : 'Inactive'} variant={u.isActive ? 'green' : 'red'} /></td>
                  <td className="px-4 py-3 text-sm text-gray-500">{new Date(u.createdAt).toLocaleDateString()}</td>
                  <td className="px-4 py-3">
                    <Button size="sm" variant={u.isActive ? 'danger' : 'secondary'} onClick={() => toggle.mutate(u.id)} loading={toggle.isPending}>
                      {u.isActive ? 'Deactivate' : 'Activate'}
                    </Button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
