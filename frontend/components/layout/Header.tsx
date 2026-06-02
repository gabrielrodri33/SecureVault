'use client';
import { Search, Plus } from 'lucide-react';
import { Button } from '@/components/ui/Button';

interface HeaderProps {
  title: string;
  search?: string;
  onSearchChange?: (v: string) => void;
  onAdd?: () => void;
  addLabel?: string;
}

export function Header({ title, search, onSearchChange, onAdd, addLabel = 'Add' }: HeaderProps) {
  return (
    <div className="flex items-center justify-between mb-6">
      <h1 className="text-2xl font-bold text-gray-900">{title}</h1>
      <div className="flex items-center gap-3">
        {onSearchChange && (
          <div className="relative">
            <Search className="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400" size={16} />
            <input value={search} onChange={e => onSearchChange(e.target.value)} placeholder="Search..." className="pl-9 pr-4 py-2 border border-gray-300 rounded-lg text-sm focus:ring-2 focus:ring-blue-500 focus:outline-none w-64" />
          </div>
        )}
        {onAdd && <Button onClick={onAdd} size="sm"><Plus size={16} className="mr-1" />{addLabel}</Button>}
      </div>
    </div>
  );
}
