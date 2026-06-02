'use client';
import { Trash2, FolderOpen } from 'lucide-react';
import { Collection } from '@/types';
import { useDeleteCollection } from '@/hooks/useCollections';
import Link from 'next/link';

export function CollectionCard({ collection }: { collection: Collection }) {
  const deleteCol = useDeleteCollection();
  return (
    <div className="bg-white rounded-xl border border-gray-200 p-4 hover:border-blue-300 hover:shadow-md transition-all group">
      <Link href={`/collections/${collection.id}`} className="flex items-center gap-3">
        <div className="h-10 w-10 rounded-lg bg-indigo-100 flex items-center justify-center">
          <FolderOpen className="text-indigo-600" size={20} />
        </div>
        <div className="flex-1 min-w-0">
          <p className="font-medium text-gray-900">{collection.name}</p>
          {collection.description && <p className="text-sm text-gray-500 truncate">{collection.description}</p>}
          <p className="text-xs text-gray-400">{collection.itemCount} items</p>
        </div>
      </Link>
      <div className="flex justify-end mt-2 opacity-0 group-hover:opacity-100 transition-opacity">
        <button onClick={() => { if (confirm('Delete collection?')) deleteCol.mutate(collection.id); }} className="p-1.5 text-gray-400 hover:text-red-600 hover:bg-red-50 rounded">
          <Trash2 size={14} />
        </button>
      </div>
    </div>
  );
}
