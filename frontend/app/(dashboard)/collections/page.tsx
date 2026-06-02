'use client';
import { useState } from 'react';
import { useCollections } from '@/hooks/useCollections';
import { CollectionCard } from '@/components/collections/CollectionCard';
import { CollectionForm } from '@/components/collections/CollectionForm';
import { Modal } from '@/components/ui/Modal';
import { Header } from '@/components/layout/Header';

export default function CollectionsPage() {
  const { data, isLoading } = useCollections();
  const [showForm, setShowForm] = useState(false);
  return (
    <div>
      <Header title="Collections" onAdd={() => setShowForm(true)} addLabel="New Collection" />
      {isLoading ? <div className="flex justify-center py-12"><div className="animate-spin h-8 w-8 border-4 border-blue-600 border-t-transparent rounded-full" /></div> : (
        <>
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
            {data?.map(col => <CollectionCard key={col.id} collection={col} />)}
          </div>
          {(!data || data.length === 0) && <div className="text-center py-12 text-gray-400">No collections yet.</div>}
        </>
      )}
      <Modal open={showForm} onClose={() => setShowForm(false)} title="New Collection">
        <CollectionForm onClose={() => setShowForm(false)} />
      </Modal>
    </div>
  );
}
