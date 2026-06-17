import React from 'react';
import styles from './Badge.module.css';

interface BadgeProps {
  children: React.ReactNode;
  variant?: 'active' | 'inactive' | 'warning' | 'error';
  className?: string;
}

export function Badge({ children, variant = 'active', className = '' }: BadgeProps) {
  return (
    <span className={`${styles.badge} ${styles[variant]} ${className}`}>
      <span className={styles.dot}></span>
      {children}
    </span>
  );
}
