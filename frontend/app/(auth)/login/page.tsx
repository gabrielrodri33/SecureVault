'use client';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import Link from 'next/link';
import { useRouter } from 'next/navigation';
import { Shield } from 'lucide-react';
import { useLogin } from '@/hooks/useAuth';
import { Input } from '@/components/ui/Input';
import { Button } from '@/components/ui/Button';

const schema = z.object({ email: z.string().email(), password: z.string().min(1) });
type FormValues = z.infer<typeof schema>;

export default function LoginPage() {
  const router = useRouter();
  const login = useLogin();
  const { register, handleSubmit, formState: { errors } } = useForm<FormValues>({ resolver: zodResolver(schema) });

  const onSubmit = (v: FormValues) => login.mutate(v, { onSuccess: () => router.push('/') });

  return (
    <div className="min-h-screen bg-gray-50 flex items-center justify-center p-4">
      <div className="w-full max-w-md">
        <div className="text-center mb-8">
          <div className="inline-flex items-center justify-center h-12 w-12 rounded-xl bg-blue-600 mb-4">
            <Shield className="text-white" size={24} />
          </div>
          <h1 className="text-2xl font-bold text-gray-900">SecureVault</h1>
          <p className="text-gray-500 mt-1">Sign in to your account</p>
        </div>
        <div className="bg-white rounded-2xl shadow-sm border border-gray-200 p-8">
          <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
            <Input label="Email" id="email" type="email" autoComplete="email" {...register('email')} error={errors.email?.message} />
            <Input label="Password" id="password" type="password" autoComplete="current-password" {...register('password')} error={errors.password?.message} />
            {login.error && <p className="text-sm text-red-600">Invalid email or password.</p>}
            <Button type="submit" className="w-full" size="lg" loading={login.isPending}>Sign In</Button>
          </form>
          <p className="text-center text-sm text-gray-500 mt-6">Don&apos;t have an account? <Link href="/register" className="text-blue-600 hover:underline">Register</Link></p>
        </div>
      </div>
    </div>
  );
}
