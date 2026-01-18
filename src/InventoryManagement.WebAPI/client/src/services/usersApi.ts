import { PagedResult } from '@/types/pageResult';
import { User } from '@/types/user';
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000/api';

export const usersApi = createApi({
    reducerPath : 'usersApi',
    baseQuery: fetchBaseQuery({ baseUrl: API_BASE_URL}),
    tagTypes:['Users'],
    endpoints:(build) => 
    ({
        getUsers: build.query<PagedResult<User>,{filter? : string; pageNumber? : number; pageSize?: number}>
        ({
            query: ({ filter='', pageNumber=1, pageSize= 12})=> `/users/paging?filter=${encodeURIComponent(filter)}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
            providesTags: (result) => 
                result 
                ?[...result.items.map(
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