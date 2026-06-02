'use client';
import { useState } from 'react';
import { Star, Trash2, Copy, Check } from 'lucide-react';
import { VaultItem } from '@/types';
import { useToggleFavorite, useDeleteVaultItem } from '@/hooks/useVault';
import { clsx } from 'clsx';

interface VaultItemCardProps {
  item: VaultItem;
  onClick: () => void;
}

export function VaultItemCard({ item, onClick }: VaultItemCardProps) {
  const [copiedUser, setCopiedUser] = useState(false);
  const toggleFavorite = useToggleFavorite();
  const deleteItem = useDeleteVaultItem();

  const copyUsername = async (e: React.MouseEvent) => {
    e.stopPropagation();
    if (!item.username) return;
    await navigator.clipboard.writeText(item.username);
    setCopiedUser(true);
    setTimeout(() => setCopiedUser(false), 2000);
  };

  return (
    <div onClick={onClick} className="bg-white rounded-xl border border-gray-200 p-4 hover:border-blue-300 hover:shadow-md transition-all cursor-pointer group">
      <div className="flex items-start justify-between">
        <div className="flex-1 min-w-0">
          <div className="flex items-center gap-2">
            <div className="h-8 w-8 rounded-lg bg-blue-100 flex items-center justify-center flex-shrink-0">
              <span className="text-blue-600 font-bold text-sm">{item.title[0].toUpperCase()}</span>
            </div>
            <div className="min-w-0">
              <p className="font-medium text-gray-900 truncate">{item.title}</p>
              {item.username && <p className="text-sm text-gray-500 truncate">{item.username}</p>}
              {item.url && <p className="text-xs text-blue-500 truncate">{item.url}</p>}
            </div>
          </div>
        </div>
        <div className="flex items-center gap-1 ml-2 opacity-0 group-hover:opacity-100 transition-opacity">
          {item.username && (
            <button onClick={copyUsername} className="p-1.5 rounded text-gray-400 hover:text-gray-700 hover:bg-gray-100" title="Copy username">
              {copiedUser ? <Check size={14} className="text-green-500" /> : <Copy size={14} />}
            </button>
          )}
          <button onClick={(e) => { e.stopPropagation(); toggleFavorite.mutate(item.id); }} className={clsx('p-1.5 rounded hover:bg-gray-100', item.isFavorite ? 'text-yellow-400' : 'text-gray-400 hover:text-yellow-400')}>
            <Star size={14} fill={item.isFavorite ? 'currentColor' : 'none'} />
          </button>
          <button onClick={(e) => { e.stopPropagation(); if (confirm('Delete this item?')) deleteItem.mutate(item.id); }} className="p-1.5 rounded text-gray-400 hover:text-red-600 hover:bg-red-50">
            <Trash2 size={14} />
          </button>
        </div>
      </div>
    </div>
  );
}
