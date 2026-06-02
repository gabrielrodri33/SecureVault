'use client';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { X } from 'lucide-react';
import { VaultItemDetail } from '@/types';
import { useCreateVaultItem, useUpdateVaultItem } from '@/hooks/useVault';
import { useCollections } from '@/hooks/useCollections';
import { Input } from '@/components/ui/Input';
import { Button } from '@/components/ui/Button';

const schema = z.object({
  title: z.string().min(1, 'Title is required'),
  username: z.string().optional(),
  password: z.string().min(1, 'Password is required'),
  url: z.string().optional(),
  notes: z.string().optional(),
  collectionId: z.string().optional(),
  isFavorite: z.boolean().optional(),
});

type FormValues = z.infer<typeof schema>;

interface VaultItemFormProps {
  item?: VaultItemDetail;
  onClose: () => void;
}

export function VaultItemForm({ item, onClose }: VaultItemFormProps) {
  const createItem = useCreateVaultItem();
  const updateItem = useUpdateVaultItem();
  const { data: collections } = useCollections();

  const { register, handleSubmit, formState: { errors } } = useForm<FormValues>({
    resolver: zodResolver(schema),
    defaultValues: item ? { title: item.title, username: item.username ?? '', password: item.password, url: item.url ?? '', notes: item.notes ?? '', collectionId: item.collectionId ?? '', isFavorite: item.isFavorite } : { isFavorite: false },
  });

  const onSubmit = (values: FormValues) => {
    const data = { ...values, collectionId: values.collectionId || undefined };
    if (item) {
      updateItem.mutate({ id: item.id, ...data, isFavorite: data.isFavorite ?? false }, { onSuccess: onClose });
    } else {
      createItem.mutate(data, { onSuccess: onClose });
    }
  };

  return (
    <div className="fixed inset-y-0 right-0 w-full max-w-lg bg-white shadow-2xl z-40 overflow-y-auto">
      <div className="p-6">
        <div className="flex items-center justify-between mb-6">
          <h2 className="text-xl font-semibold">{item ? 'Edit Item' : 'New Item'}</h2>
          <button onClick={onClose}><X size={20} /></button>
        </div>
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <Input label="Title" id="title" {...register('title')} error={errors.title?.message} />
          <Input label="Username" id="username" {...register('username')} />
          <Input label="Password" id="password" type="password" {...register('password')} error={errors.password?.message} />
          <Input label="URL" id="url" {...register('url')} />
          <div className="space-y-1">
            <label className="block text-sm font-medium text-gray-700">Notes</label>
            <textarea {...register('notes')} rows={3} className="block w-full rounded-lg border border-gray-300 px-3 py-2 text-sm focus:ring-2 focus:ring-blue-500 focus:outline-none" />
          </div>
          {collections && collections.length > 0 && (
            <div className="space-y-1">
              <label className="block text-sm font-medium text-gray-700">Collection</label>
              <select {...register('collectionId')} className="block w-full rounded-lg border border-gray-300 px-3 py-2 text-sm focus:ring-2 focus:ring-blue-500">
                <option value="">No collection</option>
                {collections.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
              </select>
            </div>
          )}
          <label className="flex items-center gap-2 text-sm"><input type="checkbox" {...register('isFavorite')} className="rounded" />Favorite</label>
          <div className="flex gap-2 pt-2">
            <Button type="submit" loading={createItem.isPending || updateItem.isPending}>{item ? 'Update' : 'Create'}</Button>
            <Button type="button" variant="secondary" onClick={onClose}>Cancel</Button>
          </div>
        </form>
      </div>
    </div>
  );
}
