import { useMutation } from '@tanstack/react-query';
import { api } from '@/lib/api';
import { useAuthStore } from '@/store/authStore';
import { AuthResponse } from '@/types';
import { useRouter } from 'next/navigation';
import { clearTokens } from '@/lib/auth';

export function useLogin() {
  const { login } = useAuthStore();
  return useMutation({
    mutationFn: (data: { email: string; password: string }) => api.post<AuthResponse>('/api/auth/login', data).then(r => r.data),
    onSuccess: (data) => { login(data.user, data.accessToken, data.refreshToken); },
  });
}

export function useRegister() {
  const { login } = useAuthStore();
  return useMutation({
    mutationFn: (data: { email: string; password: string; name: string }) => api.post<AuthResponse>('/api/auth/register', data).then(r => r.data),
    onSuccess: (data) => { login(data.user, data.accessToken, data.refreshToken); },
  });
}

export function useLogout() {
  const { logout } = useAuthStore();
  const router = useRouter();
  return () => {
    const refresh = typeof window !== 'undefined' ? localStorage.getItem('sv_refresh_token') : null;
    if (refresh) api.post('/api/auth/logout', { refreshToken: refresh }).catch(() => {});
    logout();
    clearTokens();
    router.push('/login');
  };
}
