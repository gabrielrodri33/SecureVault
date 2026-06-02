'use client';
import { useState } from 'react';
import { useVaultItems } from '@/hooks/useVault';
import { VaultItemCard } from '@/components/vault/VaultItemCard';
import { VaultItemDetail } from '@/components/vault/VaultItemDetail';
import { VaultItemForm } from '@/components/vault/VaultItemForm';
import { Header } from '@/components/layout/Header';
import { useCollections } from '@/hooks/useCollections';

export default function VaultPage() {
  const [search, setSearch] = useState('');
  const [selectedCollection, setSelectedCollection] = useState<string | undefined>();
  const [selectedId, setSelectedId] = useState<string | null>(null);
  const [showForm, setShowForm] = useState(false);
  const [page, setPage] = useState(1);

  const { data, isLoading } = useVaultItems(search || undefined, selectedCollection, page);
  const { data: collections } = useCollections();

  return (
    <div>
      <Header title="All Items" search={search} onSearchChange={v => { setSearch(v); setPage(1); }} onAdd={() => setShowForm(true)} addLabel="New Item" />
      {collections && collections.length > 0 && (
        <div className="flex gap-2 mb-4 flex-wrap">
          <button onClick={() => setSelectedCollection(undefined)} className={`px-3 py-1 rounded-full text-sm border ${!selectedCollection ? 'bg-blue-600 text-white border-blue-600' : 'border-gray-300 text-gray-600 hover:border-blue-300'}`}>All</button>
          {collections.map(c => (
            <button key={c.id} onClick={() => setSelectedCollection(c.id === selectedCollection ? undefined : c.id)} className={`px-3 py-1 rounded-full text-sm border ${selectedCollection === c.id ? 'bg-blue-600 text-white border-blue-600' : 'border-gray-300 text-gray-600 hover:border-blue-300'}`}>{c.name}</button>
          ))}
        </div>
      )}
      {isLoading ? (
        <div className="flex justify-center py-12"><div className="animate-spin h-8 w-8 border-4 border-blue-600 border-t-transparent rounded-full" /></div>
      ) : (
        <>
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
            {data?.items.map(item => <VaultItemCard key={item.id} item={item} onClick={() => setSelectedId(item.id)} />)}
          </div>
          {data && data.totalCount === 0 && <div className="text-center py-12 text-gray-400">No items yet. Create your first one!</div>}
          {data && data.totalCount > data.pageSize && (
            <div className="flex justify-center gap-2 mt-6">
              <button disabled={page === 1} onClick={() => setPage(p => p - 1)} className="px-4 py-2 border rounded-lg text-sm disabled:opacity-50">Previous</button>
              <span className="px-4 py-2 text-sm text-gray-600">Page {page}</span>
              <button disabled={page * data.pageSize >= data.totalCount} onClick={() => setPage(p => p + 1)} className="px-4 py-2 border rounded-lg text-sm disabled:opacity-50">Next</button>
            </div>
          )}
        </>
      )}
      {selectedId && <VaultItemDetail id={selectedId} onClose={() => setSelectedId(null)} />}
      {showForm && <VaultItemForm onClose={() => setShowForm(false)} />}
    </div>
  );
}
