import type { Product } from '@/types/product';
import { apiRequest } from './api';
import { PagedResult } from '@/types/pageResult';

// Product API endpoints

export const productsApi = {
    getAll: () => apiRequest<Product[]>('/products'),
    getById: (id: number) => apiRequest<Product>(`/products/${id}`),
    getPaging:(filter: string, pageNumber: number, pageSize: number) => 
        apiRequest<PagedResult<Product>>(`/products/paging?filter=${filter}&pageNumber=${pageNumber}&pageSize=${pageSize}`),
    create: (product: Omit<Product, 'id'>) => apiRequest<Product>('/products', {
        method: 'POST',
        body: JSON.stringify(product),
    }),
    update: (id: number, product: Partial<Product>) => apiRequest<Product>(`/products/${id}`, {
        method: 'PUT',
        body: JSON.stringify(product),
    }),
    delete: (id: number) => apiRequest<void>(`/products/${id}`, {
        method: 'DELETE',
    }),
};
