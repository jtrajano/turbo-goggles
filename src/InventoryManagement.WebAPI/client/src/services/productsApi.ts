import { PagedResult } from "@/types/pageResult";
import { Product } from "@/types/product";
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000/api';

export const productsApi = createApi({
    reducerPath: 'productsApi',
    baseQuery: fetchBaseQuery({
        baseUrl: API_BASE_URL
    }),
    tagTypes:['Products'],
    endpoints:(build) =>({
         getProducts: build.query<PagedResult<Product>,{filter? : string; pageNumber? : number; pageSize?: number}>
                ({
                    query: ({ filter='', pageNumber=1, pageSize= 12})=> `/products/paging?filter=${encodeURIComponent(filter)}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
                    providesTags: (result) => 
                        result 
                        ?[...result.items.map(
                                ({id})=>({type: 'Products' as const, id})
                            ),
                            { type: 'Products', id: 'LIST'}
                        ]
                        :[{type: 'Products', id: 'LIST'}]
                    
                }),
        deleteProduct: build.mutation<void, string>({
            query: (id) => ({ url: `/products/${id}`, method: 'DELETE' }),
            invalidatesTags: [{ type: 'Products', id: 'LIST' }],
        }),
    })
});


export const { useGetProductsQuery, useDeleteProductMutation } = productsApi;