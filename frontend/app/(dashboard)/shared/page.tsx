'use client';
import { useSharedWithMe } from '@/hooks/useVault';
import { Header } from '@/components/layout/Header';
import { SharedVaultItem } from '@/types';
import { Share2 } from 'lucide-react';
import { Badge } from '@/components/ui/Badge';

export default function SharedPage() {
  const { data, isLoading } = useSharedWithMe();
  return (
    <div>
      <Header title="Shared with me" />
      {isLoading ? <div className="flex justify-center py-12"><div className="animate-spin h-8 w-8 border-4 border-blue-600 border-t-transparent rounded-full" /></div> : (
        <div className="space-y-3">
          {(data as SharedVaultItem[])?.map(item => (
            <div key={item.shareId} className="bg-white rounded-xl border border-gray-200 p-4 flex items-center gap-4">
              <div className="h-10 w-10 rounded-lg bg-blue-100 flex items-center justify-center flex-shrink-0">
                <Share2 className="text-blue-600" size={18} />
              </div>
              <div className="flex-1 min-w-0">
                <p className="font-medium text-gray-900">{item.title}</p>
                {item.username && <p className="text-sm text-gray-500">{item.username}</p>}
                <p className="text-xs text-gray-400">Shared by {item.sharedBy.name}</p>
              </div>
              <Badge label={item.canEdit ? 'Can edit' : 'Read only'} variant={item.canEdit ? 'blue' : 'gray'} />
            </div>
          ))}
          {(!data || (data as SharedVaultItem[]).length === 0) && <div className="text-center py-12 text-gray-400">Nothing shared with you yet.</div>}
        </div>
      )}
    </div>
  );
}
