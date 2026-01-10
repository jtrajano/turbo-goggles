import type { User } from '@/types/user';
import { apiRequest } from './api';
import { Search } from 'lucide-react';

// User API endpoints

export const usersApi = {
    getAll: () => apiRequest<User[]>('/users'),
    getById: (id: number) => apiRequest<User>(`/users/${id}`),
    create: (user: Omit<User, 'id'>) => apiRequest<User>('/users', {
        method: 'POST',
        body: JSON.stringify(user),
    }),
    update: (id: number, user: Partial<User>) => apiRequest<User>(`/users/${id}`, {
        method: 'PUT',
        body: JSON.stringify(user),
    }),
    delete: (id: number) => apiRequest<void>(`/users/${id}`, {
        method: 'DELETE',
    }),
};
