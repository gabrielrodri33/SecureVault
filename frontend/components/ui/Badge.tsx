import { clsx } from 'clsx';
interface BadgeProps { label: string; variant?: 'green' | 'red' | 'blue' | 'gray'; }
export function Badge({ label, variant = 'gray' }: BadgeProps) {
  return (
    <span className={clsx('inline-flex items-center px-2 py-0.5 rounded-full text-xs font-medium', {
      'bg-green-100 text-green-800': variant === 'green',
      'bg-red-100 text-red-800': variant === 'red',
      'bg-blue-100 text-blue-800': variant === 'blue',
      'bg-gray-100 text-gray-800': variant === 'gray',
    })}>{label}</span>
  );
}
