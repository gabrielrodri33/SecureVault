'use client';
import { useQuery } from '@tanstack/react-query';
import { api } from '@/lib/api';
import { Stats } from '@/types';
import { Header } from '@/components/layout/Header';
import { Users, Shield, FolderOpen } from 'lucide-react';

export default function AdminStatsPage() {
  const { data } = useQuery({ queryKey: ['admin', 'stats'], queryFn: () => api.get<Stats>('/api/admin/stats').then(r => r.data) });
  const cards = [
    { label: 'Total Users', value: data?.totalUsers ?? 0, icon: Users, color: 'blue' },
    { label: 'Total Items', value: data?.totalItems ?? 0, icon: Shield, color: 'green' },
    { label: 'Total Collections', value: data?.totalCollections ?? 0, icon: FolderOpen, color: 'purple' },
  ];
  return (
    <div>
      <Header title="Statistics" />
      <div className="grid grid-cols-1 sm:grid-cols-3 gap-6">
        {cards.map(({ label, value, icon: Icon, color }) => (
          <div key={label} className="bg-white rounded-xl border border-gray-200 p-6">
            <div className={`inline-flex h-12 w-12 items-center justify-center rounded-xl bg-${color}-100 mb-4`}>
              <Icon className={`text-${color}-600`} size={24} />
            </div>
            <p className="text-3xl font-bold text-gray-900">{value}</p>
            <p className="text-gray-500 mt-1">{label}</p>
          </div>
        ))}
      </div>
    </div>
  );
}
