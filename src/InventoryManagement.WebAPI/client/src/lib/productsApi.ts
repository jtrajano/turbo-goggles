import type { Product } from '@/types/product';
import { apiRequest } from './api';

// Product API endpoints

export const productsApi = {
    getAll: () => apiRequest<Product[]>('/products'),
    getById: (id: number) => apiRequest<Product>(`/products/${id}`),
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
