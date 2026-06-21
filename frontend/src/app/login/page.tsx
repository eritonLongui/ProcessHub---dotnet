'use client';

import React, { useState } from 'react';
import { useRouter } from 'next/navigation';
import { Network, Mail, Lock, Eye, EyeOff } from 'lucide-react';
import { Card } from '@/components/ui/Card';
import { Input } from '@/components/ui/Input';
import { Button } from '@/components/ui/Button';
import styles from './login.module.css';
import { fetchApi } from '@/lib/api';

export default function LoginPage() {
  const [showPassword, setShowPassword] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const router = useRouter();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    setLoading(true);
    
    try {
      // 1. Envia os dados para o backend C#
      const response = await fetchApi<{ token: string }>('/Auth/login', {
        method: 'POST',
        body: JSON.stringify({ email, password })
      });
      
      // 2. Salva o token JWT e redireciona
      localStorage.setItem('token', response.token);
      router.push('/clients');
    } catch (err: any) {
      setError("Email ou senha inválidos. " + err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className={styles.container}>
      <div className={styles.logoContainer}>
        <div className={styles.logoIconWrapper}>
          <Network size={32} className={styles.logoIcon} />
        </div>
        <h1 className={styles.title}>ProcessHub</h1>
        <p className={styles.subtitle}>Enterprise Operations Platform</p>
      </div>

      <Card className={styles.card}>
        <h2 className={styles.cardTitle}>Sign In</h2>
        
        <form className={styles.form} onSubmit={handleLogin}>
          <div className={styles.inputGroup}>
            <Input 
              label="Email Address" 
              type="email" 
              placeholder="admin@enterprise.com"
              icon={<Mail size={18} />}
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </div>
          
          <div className={styles.inputGroup}>
            <div className={styles.passwordWrapper}>
              <Input 
                label="Password" 
                type={showPassword ? "text" : "password"} 
                placeholder="••••••••"
                icon={<Lock size={18} />}
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
              <button 
                type="button"
                className={styles.eyeButton}
                onClick={() => setShowPassword(!showPassword)}
              >
                {showPassword ? <EyeOff size={18} /> : <Eye size={18} />}
              </button>
            </div>
          </div>
          
          <div className={styles.options}>
            <label className={styles.checkboxLabel}>
              <input type="checkbox" className={styles.checkbox} />
              <span>Remember me</span>
            </label>
            <a href="#" className={styles.forgotLink}>Forgot password?</a>
          </div>

          {error && <div style={{ color: 'var(--color-error)', fontSize: '14px', marginTop: '8px' }}>{error}</div>}

          <Button type="submit" fullWidth className={styles.submitButton} disabled={loading}>
            {loading ? 'Entrando...' : 'Sign In'}
          </Button>
          
          <div className={styles.footer}>
            Need an account? <a href="#">Contact Administrator</a>
          </div>
        </form>
      </Card>
      
      <div className={styles.copyright}>
        © 2024 ProcessHub Enterprise. All rights reserved.<br/>
        <a href="#">Privacy Policy</a> · <a href="#">Terms of Service</a>
      </div>
    </div>
  );
}
