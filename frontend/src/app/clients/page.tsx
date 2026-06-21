'use client'

import React, { useEffect, useState } from 'react';
import { Plus, Search, ChevronLeft, ChevronRight } from 'lucide-react';
import { DashboardLayout } from '@/components/layout/DashboardLayout';
import { Button } from '@/components/ui/Button';
import { Badge } from '@/components/ui/Badge';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/Table';
import styles from './clients.module.css';
import { fetchApi } from '@/lib/api';

interface ClientDTO {
  id: string;
  name: string;
  email: string;
  documentNumber: string;
  isActive?: boolean;
  processes?: number;
  maxProcesses?: number;
  status?: string;
}

export default function ClientsPage() {
  const [clients, setClients] = useState<ClientDTO[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    async function loadClients() {
      try {
        const data = await fetchApi<ClientDTO[]>('/Client');
        setClients(data);
      } catch (err: any) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    }

    loadClients();
  }, []);

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

      {error && <div style={{ color: 'red', marginBottom: '16px' }}>Erro ao carregar: {error}</div>}

      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Client Name</TableHead>
            <TableHead>Email</TableHead>
            <TableHead>Document</TableHead>
            <TableHead>Status</TableHead>
            <TableHead>Actions</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {loading ? (
            <TableRow>
              <TableCell colSpan={5} style={{ textAlign: 'center' }}>Carregando dados do servidor...</TableCell>
            </TableRow>
          ) : clients.length === 0 ? (
            <TableRow>
              <TableCell colSpan={5} style={{ textAlign: 'center' }}>Nenhum cliente encontrado.</TableCell>
            </TableRow>
          ) : (
            clients.map((client) => (
              <TableRow key={client.id}>
                <TableCell><strong style={{fontWeight: 500}}>{client.name}</strong></TableCell>
                <TableCell>{client.email}</TableCell>
                <TableCell>{client.documentNumber}</TableCell>
                <TableCell>
                  <Badge variant={client.isActive !== false ? 'active' : 'inactive'}>
                    {client.isActive !== false ? 'Active' : 'Inactive'}
                  </Badge>
                </TableCell>
                <TableCell></TableCell>
              </TableRow>
            ))
          )}
        </TableBody>
      </Table>
      
      {/* Paginação mantida oculta no código para brevidade */}
    </DashboardLayout>
  );
}