'use client';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { useCreateCollection } from '@/hooks/useCollections';
import { Input } from '@/components/ui/Input';
import { Button } from '@/components/ui/Button';

const schema = z.object({ name: z.string().min(1, 'Name required'), description: z.string().optional() });
type FormValues = z.infer<typeof schema>;

export function CollectionForm({ onClose }: { onClose: () => void }) {
  const create = useCreateCollection();
  const { register, handleSubmit, formState: { errors } } = useForm<FormValues>({ resolver: zodResolver(schema) });
  return (
    <form onSubmit={handleSubmit(v => create.mutate(v, { onSuccess: onClose }))} className="space-y-4">
      <Input label="Name" id="name" {...register('name')} error={errors.name?.message} />
      <Input label="Description" id="description" {...register('description')} />
      <div className="flex gap-2">
        <Button type="submit" loading={create.isPending}>Create</Button>
        <Button type="button" variant="secondary" onClick={onClose}>Cancel</Button>
      </div>
    </form>
  );
}
