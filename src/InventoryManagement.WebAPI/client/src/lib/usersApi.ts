import type { User } from '@/types/user';
import { apiRequest } from './api';
import { Search } from 'lucide-react';
import { PagedResult } from '@/types/pageResult';

// User API endpoints

export const usersApi = {
    getAll: () => apiRequest<User[]>('/users'),
    getById: (id: string) => apiRequest<User>(`/users/${id}`),
    getPaging:(filter: string, pageNumber: number, pageSize: number) => 
            apiRequest<PagedResult<User>>(`/users/paging?filter=${filter}&pageNumber=${pageNumber}&pageSize=${pageSize}`),
    create: (user: Omit<User, 'id'>) => apiRequest<User>('/users', {
        method: 'POST',
        body: JSON.stringify(user),
    }),
    update: (id: string, user: Partial<User>) => apiRequest<User>(`/users/${id}`, {
        method: 'PUT',
        body: JSON.stringify(user),
    }),
    delete: (id: string) => apiRequest<void>(`/users/${id}`, {
        method: 'DELETE',
    }),
};
