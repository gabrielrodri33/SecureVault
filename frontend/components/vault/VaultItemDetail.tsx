'use client';
import { useState } from 'react';
import { Edit, Trash2, X } from 'lucide-react';
import { useVaultItem, useDeleteVaultItem } from '@/hooks/useVault';
import { PasswordReveal } from './PasswordReveal';
import { VaultItemForm } from './VaultItemForm';
import { Button } from '@/components/ui/Button';

interface VaultItemDetailProps {
  id: string;
  onClose: () => void;
}

export function VaultItemDetail({ id, onClose }: VaultItemDetailProps) {
  const { data: item, isLoading } = useVaultItem(id);
  const deleteItem = useDeleteVaultItem();
  const [editing, setEditing] = useState(false);

  if (isLoading) return (
    <div className="fixed inset-y-0 right-0 w-full max-w-lg bg-white shadow-2xl p-6 flex items-center justify-center z-40">
      <div className="animate-spin h-8 w-8 border-4 border-blue-600 border-t-transparent rounded-full" />
    </div>
  );

  if (!item) return null;
  if (editing) return <VaultItemForm item={item} onClose={() => { setEditing(false); onClose(); }} />;

  return (
    <div className="fixed inset-y-0 right-0 w-full max-w-lg bg-white shadow-2xl z-40 overflow-y-auto">
      <div className="p-6">
        <div className="flex items-center justify-between mb-6">
          <h2 className="text-xl font-semibold text-gray-900">{item.title}</h2>
          <button onClick={onClose} className="text-gray-400 hover:text-gray-600"><X size={20} /></button>
        </div>
        <div className="space-y-4">
          {item.username && <div><p className="text-xs text-gray-500 uppercase tracking-wider mb-1">Username</p><p className="text-gray-900">{item.username}</p></div>}
          <div><p className="text-xs text-gray-500 uppercase tracking-wider mb-1">Password</p><PasswordReveal password={item.password} /></div>
          {item.url && <div><p className="text-xs text-gray-500 uppercase tracking-wider mb-1">URL</p><a href={item.url} target="_blank" rel="noopener noreferrer" className="text-blue-500 hover:underline text-sm">{item.url}</a></div>}
          {item.notes && <div><p className="text-xs text-gray-500 uppercase tracking-wider mb-1">Notes</p><p className="text-gray-700 text-sm whitespace-pre-wrap">{item.notes}</p></div>}
        </div>
        <div className="flex gap-2 mt-8">
          <Button onClick={() => setEditing(true)} variant="secondary" size="sm"><Edit size={14} className="mr-1" />Edit</Button>
          <Button onClick={() => { if (confirm('Delete?')) { deleteItem.mutate(item.id); onClose(); } }} variant="danger" size="sm"><Trash2 size={14} className="mr-1" />Delete</Button>
        </div>
      </div>
    </div>
  );
}
