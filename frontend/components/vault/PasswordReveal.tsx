'use client';
import { useState } from 'react';
import { Eye, EyeOff, Copy, Check } from 'lucide-react';

interface PasswordRevealProps { password: string; }

export function PasswordReveal({ password }: PasswordRevealProps) {
  const [show, setShow] = useState(false);
  const [copied, setCopied] = useState(false);

  const copy = async () => {
    await navigator.clipboard.writeText(password);
    setCopied(true);
    setTimeout(() => setCopied(false), 2000);
  };

  return (
    <div className="flex items-center gap-2">
      <span className="font-mono text-sm">{show ? password : '••••••••'}</span>
      <button onClick={() => setShow(v => !v)} className="text-gray-400 hover:text-gray-600">
        {show ? <EyeOff size={16} /> : <Eye size={16} />}
      </button>
      <button onClick={copy} className="text-gray-400 hover:text-gray-600">
        {copied ? <Check size={16} className="text-green-500" /> : <Copy size={16} />}
      </button>
    </div>
  );
}
