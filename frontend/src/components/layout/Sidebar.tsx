import React from 'react';
import Link from 'next/link';
import { LayoutDashboard, Users, FileText, Settings, Network } from 'lucide-react';
import styles from './Sidebar.module.css';

export function Sidebar() {
  return (
    <aside className={styles.sidebar}>
      <div className={styles.logo}>
        <Network className={styles.logoIcon} />
        <div>
          <h2>ProcessHub</h2>
          <span>Enterprise Operations</span>
        </div>
      </div>
      
      <nav className={styles.nav}>
        <Link href="/" className={styles.navItem}>
          <LayoutDashboard size={20} />
          <span>Dashboard</span>
        </Link>
        <Link href="/processes" className={styles.navItem}>
          <Network size={20} />
          <span>Processes</span>
        </Link>
        <Link href="/clients" className={`${styles.navItem} ${styles.active}`}>
          <Users size={20} />
          <span>Clients</span>
        </Link>
        <Link href="/documents" className={styles.navItem}>
          <FileText size={20} />
          <span>Documents</span>
        </Link>
      </nav>

      <div className={styles.userProfile}>
        <div className={styles.avatar}>UA</div>
        <div className={styles.userInfo}>
          <span className={styles.userName}>User Avatar</span>
          <span className={styles.userRole}>Admin</span>
        </div>
      </div>
    </aside>
  );
}
