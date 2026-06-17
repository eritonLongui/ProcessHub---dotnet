import React, { ReactNode } from 'react';
import styles from './Table.module.css';

interface TableProps {
  children: ReactNode;
}

export function Table({ children }: TableProps) {
  return (
    <div className={styles.tableContainer}>
      <table className={styles.table}>
        {children}
      </table>
    </div>
  );
}

export function TableHeader({ children }: { children: ReactNode }) {
  return <thead className={styles.thead}>{children}</thead>;
}

export function TableBody({ children }: { children: ReactNode }) {
  return <tbody>{children}</tbody>;
}

export function TableRow({ children, className = '' }: { children: ReactNode, className?: string }) {
  return <tr className={`${styles.tr} ${className}`}>{children}</tr>;
}

export function TableHead({ children, className = '' }: { children: ReactNode, className?: string }) {
  return <th className={`${styles.th} ${className}`}>{children}</th>;
}

export function TableCell({ children, className = '' }: { children?: ReactNode, className?: string }) {
  return <td className={`${styles.td} ${className}`}>{children}</td>;
}
