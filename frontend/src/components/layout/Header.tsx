import React from 'react';
import { Search, Bell, Settings } from 'lucide-react';
import styles from './Header.module.css';

export function Header() {
  return (
    <header className={styles.header}>
      <div className={styles.searchContainer}>
        <Search size={18} className={styles.searchIcon} />
        <input 
          type="text" 
          placeholder="Search ProcessHub..." 
          className={styles.searchInput}
        />
      </div>
      
      <div className={styles.actions}>
        <button className={styles.iconButton}>
          <Bell size={20} />
        </button>
        <button className={styles.iconButton}>
          <Settings size={20} />
        </button>
        <div className={styles.avatar}></div>
      </div>
    </header>
  );
}
