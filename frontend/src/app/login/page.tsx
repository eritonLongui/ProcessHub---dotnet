'use client';

import React, { useState } from 'react';
import { useRouter } from 'next/navigation';
import { Network, Mail, Lock, Eye, EyeOff } from 'lucide-react';
import { Card } from '@/components/ui/Card';
import { Input } from '@/components/ui/Input';
import { Button } from '@/components/ui/Button';
import styles from './login.module.css';

export default function LoginPage() {
  const [showPassword, setShowPassword] = useState(false);
  const router = useRouter();

  const handleLogin = (e: React.FormEvent) => {
    e.preventDefault();
    // Navegação mockada temporária para passar da tela de login
    router.push('/clients');
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
            />
          </div>
          
          <div className={styles.inputGroup}>
            <div className={styles.passwordWrapper}>
              <Input 
                label="Password" 
                type={showPassword ? "text" : "password"} 
                placeholder="••••••••"
                icon={<Lock size={18} />}
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

          <Button type="submit" fullWidth className={styles.submitButton}>
            Sign In
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
