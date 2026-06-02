'use client';
import Link from 'next/link';
import { usePathname } from 'next/navigation';
import { Shield, Star, Share2, FolderOpen, Settings, LogOut } from 'lucide-react';
import { useAuthStore } from '@/store/authStore';
import { useLogout } from '@/hooks/useAuth';
import { clsx } from 'clsx';

const navItems = [
  { href: '/', label: 'All Items', icon: Shield },
  { href: '/favorites', label: 'Favorites', icon: Star },
  { href: '/shared', label: 'Shared with me', icon: Share2 },
  { href: '/collections', label: 'Collections', icon: FolderOpen },
];

export function Sidebar() {
  const pathname = usePathname();
  const { user } = useAuthStore();
  const logout = useLogout();

  return (
    <aside className="w-64 bg-gray-900 text-white flex flex-col h-screen sticky top-0">
      <div className="p-6 border-b border-gray-800">
        <div className="flex items-center gap-2">
          <Shield className="text-blue-400" size={24} />
          <span className="text-lg font-bold">SecureVault</span>
        </div>
      </div>
      <nav className="flex-1 p-4 space-y-1">
        {navItems.map(({ href, label, icon: Icon }) => (
          <Link key={href} href={href} className={clsx('flex items-center gap-3 px-3 py-2 rounded-lg text-sm transition-colors', pathname === href ? 'bg-blue-600 text-white' : 'text-gray-300 hover:bg-gray-800 hover:text-white')}>
            <Icon size={16} />{label}
          </Link>
        ))}
        {user?.role === 'Admin' && (
          <>
            <div className="pt-4 pb-2"><p className="text-xs uppercase tracking-wider text-gray-500">Admin</p></div>
            <Link href="/admin" className={clsx('flex items-center gap-3 px-3 py-2 rounded-lg text-sm transition-colors', pathname.startsWith('/admin') && pathname !== '/admin/stats' ? 'bg-blue-600 text-white' : 'text-gray-300 hover:bg-gray-800 hover:text-white')}>
              <Settings size={16} />Users
            </Link>
            <Link href="/admin/stats" className={clsx('flex items-center gap-3 px-3 py-2 rounded-lg text-sm transition-colors', pathname === '/admin/stats' ? 'bg-blue-600 text-white' : 'text-gray-300 hover:bg-gray-800 hover:text-white')}>
              <Settings size={16} />Stats
            </Link>
          </>
        )}
      </nav>
      <div className="p-4 border-t border-gray-800">
        <div className="flex items-center justify-between">
          <div className="min-w-0">
            <p className="text-sm font-medium text-white truncate">{user?.name}</p>
            <p className="text-xs text-gray-400 truncate">{user?.email}</p>
          </div>
          <button onClick={logout} className="p-2 text-gray-400 hover:text-white rounded-lg hover:bg-gray-800"><LogOut size={16} /></button>
        </div>
      </div>
    </aside>
  );
}
