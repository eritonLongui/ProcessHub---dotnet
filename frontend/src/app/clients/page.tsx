'use client';

import React from 'react';
import { Plus, Search, ChevronLeft, ChevronRight } from 'lucide-react';
import { DashboardLayout } from '@/components/layout/DashboardLayout';
import { Button } from '@/components/ui/Button';
import { Badge } from '@/components/ui/Badge';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/Table';
import styles from './clients.module.css';

export default function ClientsPage() {
  const clients = [
    { name: 'Acme Corporation', email: 'contact@acmecorp.com', phone: '+1 (555) 019-2834', processes: 12, maxProcesses: 30, status: 'active' },
    { name: 'Globex Dynamics', email: 'operations@globex.io', phone: '+1 (555) 847-1922', processes: 3, maxProcesses: 30, status: 'active' },
    { name: 'Initech Solutions', email: 'billing@initech.com', phone: '+44 20 7946 0958', processes: 0, maxProcesses: 30, status: 'inactive' },
    { name: 'Stark Industries', email: 'logistics@stark.com', phone: '+1 (555) 123-4567', processes: 28, maxProcesses: 30, status: 'active' }
  ];

  return (
    <DashboardLayout>
      <div className={styles.header}>
        <div>
          <h1 className={styles.title}>Clients</h1>
          <p className={styles.subtitle}>Manage client profiles, active processes, and contact information.</p>
        </div>
        
        <div className={styles.actions}>
          <div className={styles.searchBox}>
            <Search size={16} className={styles.searchIcon} />
            <input type="text" placeholder="Find by name or email..." className={styles.searchInput} />
          </div>
          <Button variant="action" className={styles.addButton}>
            <Plus size={18} style={{ marginRight: 8 }} />
            Add Client
          </Button>
        </div>
      </div>

      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Client Name</TableHead>
            <TableHead>Email</TableHead>
            <TableHead>Phone</TableHead>
            <TableHead>Active Processes</TableHead>
            <TableHead>Status</TableHead>
            <TableHead>Actions</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {clients.map((client, i) => (
            <TableRow key={i}>
              <TableCell><strong style={{fontWeight: 500}}>{client.name}</strong></TableCell>
              <TableCell>{client.email}</TableCell>
              <TableCell>{client.phone}</TableCell>
              <TableCell>
                <div className={styles.processCell}>
                  <span>{client.processes}</span>
                  <div className={styles.progressBar}>
                    <div 
                      className={styles.progressFill} 
                      style={{ 
                        width: `${(client.processes / client.maxProcesses) * 100}%`,
                        backgroundColor: client.processes === 0 ? 'transparent' : 'var(--color-primary)'
                      }}
                    ></div>
                  </div>
                </div>
              </TableCell>
              <TableCell>
                <Badge variant={client.status as 'active' | 'inactive'}>
                  {client.status.charAt(0).toUpperCase() + client.status.slice(1)}
                </Badge>
              </TableCell>
              <TableCell></TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>

      <div className={styles.pagination}>
        <span className={styles.pageInfo}>Showing 1 to 4 of 48 entries</span>
        <div className={styles.pageControls}>
          <button className={styles.pageButton}><ChevronLeft size={16} /></button>
          <button className={`${styles.pageButton} ${styles.pageActive}`}>1</button>
          <button className={styles.pageButton}>2</button>
          <button className={styles.pageButton}>3</button>
          <button className={styles.pageButton}><ChevronRight size={16} /></button>
        </div>
      </div>
    </DashboardLayout>
  );
}
