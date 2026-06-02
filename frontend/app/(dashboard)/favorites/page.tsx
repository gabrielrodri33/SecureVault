'use client';
import { useState } from 'react';
import { useFavorites } from '@/hooks/useVault';
import { VaultItemCard } from '@/components/vault/VaultItemCard';
import { VaultItemDetail } from '@/components/vault/VaultItemDetail';
import { Header } from '@/components/layout/Header';

export default function FavoritesPage() {
  const { data, isLoading } = useFavorites();
  const [selectedId, setSelectedId] = useState<string | null>(null);
  return (
    <div>
      <Header title="Favorites" />
      {isLoading ? <div className="flex justify-center py-12"><div className="animate-spin h-8 w-8 border-4 border-blue-600 border-t-transparent rounded-full" /></div> : (
        <>
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
            {data?.map(item => <VaultItemCard key={item.id} item={item} onClick={() => setSelectedId(item.id)} />)}
          </div>
          {(!data || data.length === 0) && <div className="text-center py-12 text-gray-400">No favorites yet.</div>}
        </>
      )}
      {selectedId && <VaultItemDetail id={selectedId} onClose={() => setSelectedId(null)} />}
    </div>
  );
}
