import { User } from '@/types/user';
import { createApi } from '@reduxjs/toolkit/query/react'

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000/api';

export const usersApi = createApi({
    reducerPath : 'usersApi',
    baseQuery: API_BASE_URL,
    tagTypes:['Users'],
    endpoints:(build) => 
    ({
        getUsers: build.query<{ items: User[]; totalCount: number},{search? : string; page? : number; pageSize?: number}>
        ({
            query: ({ search='', page=1, pageSize= 12})=> `/users?search=${encodeURIComponent(search)}&page=${page}&pageSize=${pageSize}`,
            providesTags: (result) => result 
            ?[
                ...result.items.map(
                    ({id})=>({type: 'Users' as const, id})
                ),
                { type: 'Users', id: 'LIST'}
            ]
            :[{type: 'Users', id: 'LIST'}]
        }),
        deleteUser: build.mutation<void, string>({
            query: (id) => ({ url: `/users/${id}`, method: 'DELETE' }),
            invalidatesTags: [{ type: 'Users', id: 'LIST' }],
        }),
    })
})

export const { useGetUsersQuery, useDeleteUserMutation } = usersApi;